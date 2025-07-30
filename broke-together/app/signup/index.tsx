import HeroSection from "@/components/UI/signup/HeroSection";
import "../../global.css";
import React, { useState } from "react";
import {
  ScrollView,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from "react-native";
import SocialButton from "@/components/UI/signup/SocialButton";
import { Ionicons } from "@expo/vector-icons";

function Signup() {
  const [isVisible, setIsVisible] = useState(false);
  const [signupData, setSignupData] = useState({
    fullName: "",
    email: "",
    password: "",
  });

  const handleChange = (field: keyof typeof signupData, value: string) => {
    setSignupData((prev) => ({ ...prev, [field]: value }));
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
            className="border bg-gray-100 border-gray-300 rounded-lg p-2 text-base"
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
            className="border bg-gray-100 border-gray-300 rounded-lg p-2 text-base"
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
          <View className="flex-row items-center border bg-gray-100 border-gray-300 rounded-lg px-3">
            <TextInput
              placeholder="Enter your password"
              value={signupData.password}
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
            Already have an account?{" "}
          </Text>
          <TouchableOpacity className=" mb-1">
            <Text className="text-purple-600 font-semibold">Sign In</Text>
          </TouchableOpacity>
        </View>
      </View>

      {/* Footer */}
    </ScrollView>
  );
}

export default Signup;
