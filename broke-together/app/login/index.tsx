import HeroSection from "@/components/UI/login/HeroSection";
import SocialButton from "@/components/UI/signup/SocialButton";
import { Ionicons } from "@expo/vector-icons";
import { Link } from "expo-router";
import React, { useState } from "react";
import {
  ScrollView,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from "react-native";

function login() {
  const [isVisible, setIsVisible] = useState(false);
  const [loginData, setLoginData] = useState({
    email: "",
    password: "",
  });

  const handleChange = (field: keyof typeof loginData, value: string) => {
    setLoginData((prev) => ({ ...prev, [field]: value }));
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
        {/* Email */}
        <View>
          <Text className="text-base font-medium text-gray-700 mb-2">
            Email
          </Text>
          <TextInput
            className="border bg-gray-100 border-gray-300 rounded-lg p-2 text-base"
            placeholder="Enter your email"
            keyboardType="email-address"
            autoCapitalize="none"
            value={loginData.email}
            onChangeText={(value) => handleChange("email", value)}
          />
        </View>

        {/* Password */}
        <View>
          <Text className="text-base font-medium text-gray-700 mb-2">
            Password
          </Text>
          <View className="flex-row items-center border bg-gray-100 border-gray-300 rounded-lg px-3">
            <TextInput
              placeholder="Enter your password"
              value={loginData.password}
              onChangeText={(value) => handleChange("password", value)}
              secureTextEntry={!isVisible}
              className="flex-1 p-2 text-base"
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
        <TouchableOpacity className="py-3 rounded-lg shadow-md">
          <Text className="text-center py-3 text-black font-semibold text-md">
            Create Account
          </Text>
        </TouchableOpacity>
        <View className="flex-row align-middle justify-center">
          <Text className="text-center text-gray-600">
            Don't have an account?{" "}
          </Text>
          <TouchableOpacity className=" mb-1">
            <Link href={"/signup"}>
              <Text className="text-purple-600 font-semibold">Sign Up</Text>
            </Link>
          </TouchableOpacity>
        </View>
      </View>

      {/* Footer */}
    </ScrollView>
  );
}

export default login;
