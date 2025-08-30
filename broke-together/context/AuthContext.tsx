import React, { createContext, useCallback, useContext, useEffect, useMemo, useState } from "react";
import { loginApi, registerApi, revokeApi, userDetailsApi } from "../api/auth"
import { clearTokens, loadTokens, saveTokens } from "../lib/tokenStorage";

type User = { id: string; email?: string; fullName?: string } | null;

type AuthContextType = {
    user: User;
    isAuthenticated: boolean;
    isBootstrapping: boolean;
    register: (fullName: string, email: string, password: string) => Promise<void>;
    login: (email: string, password: string) => Promise<void>;
    logout: () => Promise<void>;
    refreshUser: () => Promise<void>;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<React.PropsWithChildren> = ({ children }) => {
    const [user, setUser] = useState<User>(null);
    const [isBootstrapping, setBootstrapping] = useState(true);

    const bootstrap = useCallback(async () => {
        try {
            const tokens = await loadTokens();
            if (tokens.accessToken) {
                // Optional: fetch user details to confirm validity
                const me = await userDetailsApi().catch(() => null);
                if (me) setUser(me);
            }
        } finally {
            setBootstrapping(false);
        }
    }, []);

    useEffect(() => {
        bootstrap();
    }, [bootstrap]);

    const register = useCallback(async (fullName: string, email: string, password: string) => {
        const res = await registerApi({ fullName, email, password });
        if (!res.succeeded) throw new Error(res.errors?.[0] ?? "Registration failed");
        await saveTokens({
            accessToken: res.accessToken,
            accessTokenExpiresAt: res.accessTokenExpiresAt,
            refreshToken: res.refreshToken,
            refreshTokenExpiresAt: res.refreshTokenExpiresAt,
        });
        // if backend returns user, use it; else fetch
        const me = res.user ?? (await userDetailsApi());
        setUser(me ?? null);
    }, []);

    const login = useCallback(async (email: string, password: string) => {
        const res = await loginApi({ email, password });
        if (!res.succeeded) throw new Error(res.errors?.[0] ?? "Login failed");
        await saveTokens({
            accessToken: res.accessToken,
            accessTokenExpiresAt: res.accessTokenExpiresAt,
            refreshToken: res.refreshToken,
            refreshTokenExpiresAt: res.refreshTokenExpiresAt,
        });
        const me = res.user ?? (await userDetailsApi());
        setUser(me ?? null);
    }, []);

    const logout = useCallback(async () => {
        try {
            await revokeApi();
        } catch {
            // ignore
        } finally {
            await clearTokens();
            setUser(null);
        }
    }, []);

    const refreshUser = useCallback(async () => {
        const me = await userDetailsApi();
        setUser(me ?? null);
    }, []);

    const value = useMemo(
        () => ({
            user,
            isAuthenticated: !!user,
            isBootstrapping,
            register,
            login,
            logout,
            refreshUser,
        }),
        [user, isBootstrapping, register, login, logout, refreshUser]
    );

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export function useAuth() {
    const ctx = useContext(AuthContext);
    if (!ctx) throw new Error("useAuth must be used within AuthProvider");
    return ctx;
}