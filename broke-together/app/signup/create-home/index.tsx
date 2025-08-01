import { Ionicons } from "@expo/vector-icons";
import { router } from "expo-router";
import React, { useState } from "react";
import { Text, TextInput, TouchableOpacity, View } from "react-native";

function CreateHome() {
  const [homeName, setHomeName] = useState<string>("");
  const isDisabled = !homeName.trim();
  return (
    <View className="m-4 flex-1    justify-center">
      <View className="m-4 flex-row items-center justify-center">
        <TouchableOpacity onPress={() => router.back()} className="">
          <Ionicons name="chevron-back" size={28} color="#333" />
        </TouchableOpacity>
        <View className="m-4 flex-1  justify-center">
          <Text className="text-gray-600 text-2xl font-bold">
            Create Your Home
          </Text>
          <Text className="text-gray-500 text-md ">Give your home a name.</Text>
        </View>
      </View>
      <View className="flex-col gap-3 items-start justify-start m-4">
        <Text className="text-sm text-gray-500  font-bold">Home Name</Text>
        <TextInput
          className="bg-gray-200 w-full p-3 rounded-lg"
          placeholder="e.g Elm Street House, The Squad....."
          value={homeName}
          onChangeText={(value) => setHomeName(value)}
        />
        <TouchableOpacity
          className={`flex items-center max-w-min w-full p-3 rounded-lg ${isDisabled ? "bg-[#a3b18a6a]" : "bg-[#a3b18ae8]"} `}
          disabled={isDisabled}
          onPress={()=>router.push("/signup/create-home/invite-home")}
        >
          <Text className="text-white">Create Home</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
}

export default CreateHome;
