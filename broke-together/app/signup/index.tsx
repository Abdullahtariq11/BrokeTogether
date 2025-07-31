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
import { Link } from "expo-router";

// Define the type for our form data for better type safety
type SignupDataType = {
  fullName: string;
  email: string;
  password: string;
};

function Signup() {
  const [isVisible, setIsVisible] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [signupData, setSignupData] = useState<SignupDataType>({
    fullName: "",
    email: "",
    password: "",
  });


  // State to manually track focus for the password input's container
  const [isPasswordFocused, setIsPasswordFocused] = useState<boolean>(false);
 

  const handleChange = (field: keyof SignupDataType, value: string) => {
    setSignupData((prev) => ({ ...prev, [field]: value }));
  };

  const handleCreateAccount = () => {
    setIsLoading(true);
    setTimeout(() => {
      setIsLoading(false);
      console.log("Creating account with:", signupData);
    }, 2000);
  };

  return (
    <ScrollView className="flex-1 bg-white">
      {/* Header Section */}
      <HeroSection />

      {/* Social Login Buttons */}
      <SocialButton />

      {/* Divider */}
      <View className="flex-row items-center my-8 mx-6">
        <View className="flex-1 h-px bg-gray-300" />
        <Text className="mx-3 text-gray-500 text-sm">
          Or continue with email
        </Text>
        <View className="flex-1 h-px bg-gray-300" />
      </View>

      {/* Form Container */}
      <View className="mx-6 mb-5 bg-white border border-gray-200 rounded-2xl shadow-md p-6 space-y-6">
        {/* Full Name */}
        <View>
          <Text className="text-base font-medium text-gray-700 mb-1">
            Full Name
          </Text>
          <TextInput
            className="border bg-gray-50 border-gray-300 rounded-lg p-3 text-base focus:border-green-500"
            placeholder="Enter your name"
            value={signupData.fullName}
            onChangeText={(value) => handleChange("fullName", value)}
          />
        </View>

        {/* Email */}
        <View>
          <Text className="text-base font-medium text-gray-700 mb-2">
            Email
          </Text>
          <TextInput
            className="border bg-gray-50 border-gray-300 rounded-lg p-3 text-base focus:border-green-500"
            placeholder="Enter your email"
            keyboardType="email-address"
            autoCapitalize="none"
            value={signupData.email}
            onChangeText={(value) => handleChange("email", value)}
          />
        </View>

        {/* Password */}
        <View>
          <Text className="text-base font-medium text-gray-700 mb-2">
            Password
          </Text>
          {/* --- FIX START --- */}
          {/* Conditionally apply border style based on focus state */}
          <View
            className={`flex-row items-center border bg-gray-50 rounded-lg px-3 ${
              isPasswordFocused ? "border-green-500" : "border-gray-300"
            }`}
          >
          {/* --- FIX END --- */}
            <TextInput
              placeholder="Enter your password"
              value={signupData.password}
              onChangeText={(value) => handleChange("password", value)}
              secureTextEntry={!isVisible}
              className="flex-1 py-3 text-base"
              // --- FIX START ---
              // Set the focus state for the container
              onFocus={() => setIsPasswordFocused(true)}
              onBlur={() => setIsPasswordFocused(false)}
              // --- FIX END ---
            />
            <TouchableOpacity onPress={() => setIsVisible(!isVisible)}>
              <Ionicons
                name={isVisible ? "eye-off" : "eye"}
                size={22}
                color="gray"
              />
            </TouchableOpacity>
          </View>
        </View>

        {/* Continue Button */}
        <TouchableOpacity
          onPress={handleCreateAccount}
          className="mt-4 mb-1 py-3 rounded-lg shadow-md bg-[#E98074] flex-row justify-center items-center"
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
        
        <View className="flex-row items-center justify-center">
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
