import HeroSection from "@/components/UI/login/HeroSection";
import SocialButton from "@/components/UI/signup/SocialButton";
import { Ionicons } from "@expo/vector-icons";
import { Link, router } from "expo-router";
import React, { useState } from "react";
import {
  ScrollView,
  Text,
  TextInput,
  TouchableOpacity,
  View,
  ActivityIndicator,
} from "react-native";

// Validation
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";

// ✅ import the auth hook
import { useAuth } from "../../context/AuthContext";

const loginSchema = z.object({
  email: z.string().email("Please enter a valid email address"),
  password: z.string().min(8, "Password must be at least 8 characters"),
});
type LoginDataType = z.infer<typeof loginSchema>;

function Login() {
  const { login } = useAuth(); // ✅ get login action

  const [isVisible, setIsVisible] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [isPasswordFocused, setIsPasswordFocused] = useState(false);
  const [serverError, setServerError] = useState<string | null>(null); // ✅ show API errors

  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginDataType>({
    resolver: zodResolver(loginSchema),
  });

  // ✅ actually hit your API via auth context
  const onSubmit = async (data: LoginDataType) => {
    setServerError(null);
    setIsLoading(true);
    try {
      await login(data.email.trim(), data.password);
      // Go to your main screen (adjust the route to your tabs/home)
      router.replace("/dashboard"); // or router.replace("/(tabs)");
    } catch (err: any) {
      // Axios/validation error messages
      const msg =
        err?.response?.data?.message ||
        err?.message ||
        "Login failed. Please try again.";
      setServerError(msg);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <ScrollView className="flex-1 bg-white" keyboardShouldPersistTaps="handled">
      <HeroSection />
      <SocialButton />

      <View className="flex-row items-center my-8 mx-6">
        <View className="flex-1 h-px bg-gray-300" />
        <Text className="mx-3 text-gray-500 text-sm">Or continue with email</Text>
        <View className="flex-1 h-px bg-gray-300" />
      </View>

      <View className="mx-6 mb-5 bg-white border border-gray-200 rounded-2xl shadow-md p-6 space-y-6">
        {/* ✅ server error */}
        {serverError && (
          <View className="bg-red-50 border border-red-200 rounded-lg p-3">
            <Text className="text-red-600">{serverError}</Text>
          </View>
        )}

        {/* Email */}
        <View>
          <Text className="text-base font-medium text-gray-700 mb-2">Email</Text>
          <Controller
            control={control}
            name="email"
            render={({ field: { onChange, onBlur, value } }) => (
              <TextInput
                className={`border rounded-lg p-3 text-base ${
                  errors.email
                    ? "border-red-500"
                    : "border-gray-300 bg-gray-50 focus:border-green-500"
                }`}
                placeholder="Enter your email"
                keyboardType="email-address"
                autoCapitalize="none"
                onBlur={onBlur}
                onChangeText={onChange}
                value={value}
              />
            )}
          />
          {errors.email && (
            <Text className="text-red-500 mt-1">{errors.email.message}</Text>
          )}
        </View>

        {/* Password */}
        <View>
          <Text className="text-base font-medium text-gray-700 mb-2">Password</Text>
          <Controller
            control={control}
            name="password"
            render={({ field: { onChange, onBlur, value} }) => (
              <View
                className={`flex-row items-center border rounded-lg px-3 ${
                  errors.password
                    ? "border-red-500"
                    : isPasswordFocused
                    ? "border-green-500"
                    : "border-gray-300 bg-gray-50"
                }`}
              >
                <TextInput
                  placeholder="Enter your password"
                  value={value}
                  onChangeText={onChange}
                  secureTextEntry={!isVisible}
                  className="flex-1 py-3 text-base"
                  onBlur={() => {
                    onBlur();
                    setIsPasswordFocused(false);
                  }}
                  onFocus={() => setIsPasswordFocused(true)}
                />
                <TouchableOpacity onPress={() => setIsVisible(!isVisible)}>
                  <Ionicons
                    name={isVisible ? "eye-off" : "eye"}
                    size={22}
                    color="gray"
                  />
                </TouchableOpacity>
              </View>
            )}
          />
          {errors.password && (
            <Text className="text-red-500 mt-1">{errors.password.message}</Text>
          )}
        </View>

        {/* Login Button */}
        <TouchableOpacity
          onPress={handleSubmit(onSubmit)}
          className="py-3 mt-4 rounded-lg shadow-md bg-[#E98074] flex-row justify-center items-center"
          disabled={isLoading}
        >
          {isLoading ? (
            <ActivityIndicator size="small" color="white" />
          ) : (
            <Text className="text-center text-white font-semibold text-md">
              Log In
            </Text>
          )}
        </TouchableOpacity>

        {/* Footer Link */}
        <View className="flex-row items-center justify-center pt-2">
          <Text className="text-center text-gray-600">Don't have an account? </Text>
          <Link href={"/signup"} asChild>
            <TouchableOpacity>
              <Text className="text-green-600 font-semibold">Sign Up</Text>
            </TouchableOpacity>
          </Link>
        </View>
      </View>
    </ScrollView>
  );
}

export default Login;