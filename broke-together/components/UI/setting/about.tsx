import { Ionicons } from "@expo/vector-icons";
import React from "react";
import { View, Text } from "react-native";

function About() {
  return (
    <View className="bg-white p-6 rounded-2xl shadow-md mt-4">
      {/* Title */}
      <Text className="text-xl font-bold text-gray-800 mb-4">About</Text>

      {/* App Info */}
      <View className="flex-row justify-between py-3 border-b border-gray-200">
        <Text className="text-gray-600 text-base">App</Text>
        <Text className="text-gray-800 font-medium">Broke Together</Text>
      </View>

      <View className="flex-row justify-between py-3">
        <Text className="text-gray-600 text-base">Version</Text>
        <Text className="text-gray-800 font-medium">1.0.0</Text>
      </View>
    </View>
  );
}

export default About;