import { Ionicons } from "@expo/vector-icons";
import React from "react";
import { Text, TouchableOpacity, View } from "react-native";

function InviteHome() {
  const homeName = "HomeName"; 
  const inviteCode = "485435";

  return (
    <View className="flex-1 items-center justify-center bg-white px-6 pt-16">
      {/* Title Section */}
      <View className="items-center mb-10">
        <Text className="text-2xl font-bold text-gray-800 text-center">
          Invite your roommates to "{homeName}"
        </Text>
        <Text className="text-base text-gray-500 mt-3 text-center">
          Share this code so they can join your home.
        </Text>
      </View>

      {/* Invite Code Card */}
      <View className="w-full items-center bg-gray-100 rounded-2xl p-6 shadow-md mb-8">
        <Text className="text-sm text-gray-500 mb-3">Your Invite Code</Text>
        <View className="flex-row items-center border border-gray-300 rounded-lg px-6 py-4 bg-white">
          <Text className="text-4xl font-extrabold text-gray-900 tracking-widest mr-4">
            {inviteCode}
          </Text>
          <TouchableOpacity>
            <Ionicons name="copy-outline" size={28} color="#333" />
          </TouchableOpacity>
        </View>
        <Text className="text-sm text-gray-400 mt-4 italic">Tap to Copy</Text>
      </View>

      {/* Share Button */}
      <TouchableOpacity className="flex-row items-center justify-center w-full bg-[#E98074] p-4 rounded-lg shadow-md mb-6">
        <Ionicons name="share-social-outline" size={18} color="white" style={{ marginRight: 8 }} />
        <Text className="text-white font-semibold text-lg">Share Invite Link</Text>
      </TouchableOpacity>

      {/* Household Members Card */}
      <View className="w-full bg-white p-5 border border-gray-200 rounded-2xl shadow-sm mb-8">
        <View className="flex-row items-center mb-4">
          <Ionicons name="people-outline" size={20} color="#333" />
          <Text className="ml-2 text-gray-700 font-semibold text-lg">Household Members</Text>
        </View>
        <View className="flex-row items-center">
          <Ionicons name="person-circle-outline" size={28} color="#A3B18A" />
          <Text className="ml-2 text-gray-600 text-base">You</Text>
        </View>
      </View>

      {/* Continue Button */}
      <TouchableOpacity className="w-full bg-[#A3B18A] p-4 rounded-lg shadow-md">
        <Text className="text-white text-center font-semibold text-lg">Let’s Go!</Text>
      </TouchableOpacity>

      {/* Ending Note */}
      <Text className="text-sm text-gray-500 mt-10 text-center">
        You can invite more people later from the settings.
      </Text>
    </View>
  );
}

export default InviteHome;