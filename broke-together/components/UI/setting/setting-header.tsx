import React from "react";
import { Text, View, TouchableOpacity } from "react-native";
import { Ionicons } from "@expo/vector-icons";
import { SafeAreaView } from "react-native-safe-area-context";
import { router } from "expo-router";

function SettingHeader() {
  return (
    <SafeAreaView edges={["top"]} className="bg-white">
      <View className="flex-row gap-4 justify-start items-center w-full border-b border-gray-200 px-4 py-4 shadow-sm">
        <TouchableOpacity onPress={()=>router.back()}>
          <Ionicons name="arrow-back" size={26} color="#555" />
        </TouchableOpacity>
        <Text className="text-gray-800 text-2xl">Settings</Text>
      </View>
    </SafeAreaView>
  );
}

export default SettingHeader;
