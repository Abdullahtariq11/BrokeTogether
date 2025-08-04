import About from "@/components/UI/setting/about";
import HouseHold from "@/components/UI/setting/house-hold";
import Profile from "@/components/UI/setting/profile";
import SettingHeader from "@/components/UI/setting/setting-header";
import React from "react";
import { ScrollView, Text, TouchableOpacity, View } from "react-native";

function Settings() {
  return (
    <View className="flex-1">
      {/* Header */}
      <SettingHeader />

      <ScrollView
        className="flex-1 px-4"
        contentContainerStyle={{ paddingBottom: 40 }}
        showsVerticalScrollIndicator={false}
      >
        {/* Profile Section */}
        <Profile />

        {/* Household Section */}
        <HouseHold />

        {/* About Section */}
        <About />

        {/* Logout Button */}
        <TouchableOpacity className="mt-6 bg-red-500 p-4 rounded-xl shadow-md">
          <Text className="text-center text-white font-semibold text-lg">
            Log Out
          </Text>
        </TouchableOpacity>
      </ScrollView>
    </View>
  );
}

export default Settings;
