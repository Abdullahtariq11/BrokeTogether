import { Ionicons } from "@expo/vector-icons";
import { router } from "expo-router";
import React, { useState } from "react";
import { Text, TextInput, TouchableOpacity, View } from "react-native";

function index() {
  const [joinName, setJoinName] = useState<string>("");
  const isDisabled = !joinName.trim();
  return (
    <View className="m-4 flex-1    justify-center">
      <View className="m-4 flex-row items-center justify-center">
        <TouchableOpacity onPress={() => router.back()} className="">
          <Ionicons name="chevron-back" size={28} color="#333" />
        </TouchableOpacity>
        <View className="m-4 flex-1  justify-center">
          <Text className="text-gray-600 text-2xl font-bold">Join Home</Text>
          <Text className="text-gray-500 text-md ">Enter your invite code</Text>
        </View>
      </View>
      <View className="flex-col gap-3 items-start justify-start m-4">
        <Text className="text-sm text-gray-500  font-bold">Invite Code</Text>
        <TextInput
          className="bg-gray-200 text-center w-full p-3 rounded-lg"
          placeholder="ABC563"
          value={joinName}
          onChangeText={(value) => setJoinName(value)}
        />
        <TouchableOpacity
          className={`flex items-center max-w-min w-full p-3 rounded-lg ${isDisabled ? "bg-[#a3b18a6a]" : "bg-[#a3b18ae8]"} `}
          disabled={isDisabled}
          onPress={() => router.replace("/dashboard")}
        >
          <Text className="text-white">Join Home</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
}

export default index;
