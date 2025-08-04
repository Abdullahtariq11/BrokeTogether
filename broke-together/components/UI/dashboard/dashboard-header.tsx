import React from "react";
import { Text, View, TouchableOpacity } from "react-native";
import { Ionicons } from "@expo/vector-icons";
import { SafeAreaView } from "react-native-safe-area-context";
import { router } from "expo-router";

function DashboardHeader() {
  return (
    <SafeAreaView edges={['top']} className="bg-white">
      <View className="flex-row justify-between items-center w-full border-b border-gray-200 px-4 py-4 shadow-sm">
        <Text className="text-gray-800 text-2xl font-extrabold">
          Elm Street
        </Text>
        <TouchableOpacity onPress={()=>router.push("/(app)/settings")}>
          <Ionicons name="settings-outline" size={26} color="#555" />
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
}

export default DashboardHeader;