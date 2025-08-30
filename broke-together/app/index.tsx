import HeroSection from "@/components/UI/signup/HeroSection";
import "../global.css";
import React, { useState } from "react";
import {
  ScrollView,
  Text,
  TextInput,
  TouchableOpacity,
  View,
  ActivityIndicator,
} from "react-native";
import SocialButton from "@/components/UI/signup/SocialButton";
import { Ionicons } from "@expo/vector-icons";
import { Link, router } from "expo-router";

// form + validation
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";

// ✅ import the auth hook
import { useAuth } from "../context/AuthContext";

const signupSchema = z.object({
  fullName: z.string().min(2, "Full name must be at least 2 characters"),
  email: z.string().email("Please enter a valid email address"),
  password: z.string().min(8, "Password must be at least 8 characters"),
});
type SignupDataType = z.infer<typeof signupSchema>;

function Signup() {
  const { register: registerUser } = useAuth(); // ✅ from AuthContext

  const [isVisible, setIsVisible] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isPasswordFocused, setIsPasswordFocused] = useState<boolean>(false);
  const [serverError, setServerError] = useState<string | null>(null); // ✅ show API errors

  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<SignupDataType>({
    resolver: zodResolver(signupSchema),
  });

  // ✅ Call backend via auth context
  const onSubmit = async (data: SignupDataType) => {
    setServerError(null);
    setIsLoading(true);
    try {
      await registerUser(data.fullName.trim(), data.email.trim(), data.password);
      router.push("/signup/home-setup"); // continue onboarding
    } catch (err: any) {
      const msg =
        err?.response?.data?.message ||
        err?.message ||
        "Sign up failed. Please try again.";
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

      <View className="mx-6 mb-5 bg-white border border-gray-200 rounded-2xl shadow-md p-6 space-y-4">

        {/* ✅ server error */}
        {serverError && (
          <View className="bg-red-50 border border-red-200 rounded-lg p-3">
            <Text className="text-red-600">{serverError}</Text>
          </View>
        )}

        {/* Full Name */}
        <View>
          <Text className="text-base font-medium text-gray-700 mb-1">Full Name</Text>
          <Controller
            control={control}
            name="fullName"
            render={({ field: { onChange, onBlur, value } }) => (
              <TextInput
                className={`border rounded-lg p-3 text-base ${
                  errors.fullName
                    ? "border-red-500"
                    : "border-gray-300 bg-gray-50 focus:border-green-500"
                }`}
                placeholder="Enter your name"
                onBlur={onBlur}
                onChangeText={onChange}
                value={value}
              />
            )}
          />
          {errors.fullName && (
            <Text className="text-red-500 mt-1">{errors.fullName.message}</Text>
          )}
        </View>

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
            render={({ field: { onChange, onBlur, value } }) => (
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

        <TouchableOpacity
          onPress={handleSubmit(onSubmit)}
          className="py-3 mt-4 rounded-lg shadow-md bg-[#E98074] flex-row justify-center items-center"
          disabled={isLoading}
        >
          {isLoading ? (
            <ActivityIndicator size="small" color="white" />
          ) : (
            <Text className="text-center text-white font-semibold text-md">
              Create Account
            </Text>
          )}
        </TouchableOpacity>

        <View className="flex-row items-center justify-center pt-2">
          <Text className="text-center text-gray-600">Already have an account? </Text>
          <Link href={"/login"} asChild>
            <TouchableOpacity>
              <Text className="text-green-600 font-semibold">Sign In</Text>
            </TouchableOpacity>
          </Link>
        </View>
      </View>
    </ScrollView>
  );
}

export default Signup;