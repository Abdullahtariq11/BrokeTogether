import HeroSection from "@/components/UI/signup/HeroSection";
import "../../global.css";
import React from "react";
import { ScrollView, Text, View } from "react-native";
import { AntDesign } from "@expo/vector-icons"; // for Google (or FontAwesome, etc.)
import SocialButton from "@/components/UI/signup/SocialButton";

function index() {
  return (
    <ScrollView className="flex-1 bg-white ">
      {/*Header */}
      <HeroSection />
      {/*SOcial button */}
      <SocialButton />
      {/*Divider */}
      {/* Divider */}
      <View className="flex-row items-center my-6">
        <View className="flex-1 h-px bg-gray-300" />
        <Text className="mx-3 text-gray-500 text-sm">
          Or continue with email
        </Text>
        <View className="flex-1 h-px bg-gray-300" />
      </View>
      {/*Progress step */}
      {/*Form field */}
      {/*Continue Button */}
      {/*Footer */}
      <Text>Hello</Text>
    </ScrollView>
  );
}

export default index;
