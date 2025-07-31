import HeroSection from "@/components/UI/signup/HeroSection";
import "../../global.css";
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

// --- VALIDATION IMPORTS START ---
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
// --- VALIDATION IMPORTS END ---

// --- 1. DEFINE VALIDATION SCHEMA WITH ZOD ---
const signupSchema = z.object({
  fullName: z.string().min(2, "Full name must be at least 2 characters"),
  email: z.string().email("Please enter a valid email address"),
  password: z.string().min(8, "Password must be at least 8 characters"),
});

// Infer the type from the schema for type safety
type SignupDataType = z.infer<typeof signupSchema>;

function Signup() {
  const [isVisible, setIsVisible] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isPasswordFocused, setIsPasswordFocused] = useState<boolean>(false);

  // --- 2. SETUP REACT-HOOK-FORM ---
  const {
    control, // To connect inputs
    handleSubmit, // To handle form submission
    formState: { errors }, // To access validation errors
  } = useForm<SignupDataType>({
    resolver: zodResolver(signupSchema), // Use Zod for validation
  });

  // --- 3. CREATE THE SUBMISSION HANDLER ---
  // This function only runs if validation is successful
  const onSubmit = (data: SignupDataType) => {
    setIsLoading(true);
    // Simulate an API call
    setTimeout(() => {
      setIsLoading(false);
      console.log("Account created with validated data:", data);
      // Navigate to the next step in the onboarding flow.
      router.replace("/create-home");
    }, 1500);
  };

  return (
    <ScrollView className="flex-1 bg-white" keyboardShouldPersistTaps="handled">
      <HeroSection />
      <SocialButton />
      <View className="flex-row items-center my-8 mx-6">
        <View className="flex-1 h-px bg-gray-300" />
        <Text className="mx-3 text-gray-500 text-sm">
          Or continue with email
        </Text>
        <View className="flex-1 h-px bg-gray-300" />
      </View>

      <View className="mx-6 mb-5 bg-white border border-gray-200 rounded-2xl shadow-md p-6 space-y-4">
        {/* Full Name */}
        <View>
          <Text className="text-base font-medium text-gray-700 mb-1">
            Full Name
          </Text>
          {/* --- 4. USE CONTROLLER FOR EACH INPUT --- */}
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
          {/* --- 5. DISPLAY VALIDATION ERRORS --- */}
          {errors.fullName && (
            <Text className="text-red-500 mt-1">{errors.fullName.message}</Text>
          )}
        </View>

        {/* Email */}
        <View>
          <Text className="text-base font-medium text-gray-700 mb-2">
            Email
          </Text>
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
          <Text className="text-base font-medium text-gray-700 mb-2">
            Password
          </Text>
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

        {/* --- 6. USE HANDLESUBMIT FOR THE BUTTON --- */}
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
          <Text className="text-center text-gray-600">
            Already have an account?{" "}
          </Text>
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
