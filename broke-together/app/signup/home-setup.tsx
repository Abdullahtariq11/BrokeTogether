import React from "react";
import { Text, TouchableOpacity, View } from "react-native";
import { Ionicons, MaterialCommunityIcons } from "@expo/vector-icons";
import { useRouter } from "expo-router";

function HomeSetup() {
  const router = useRouter();
  return (
    <View className="m-4 flex-1  justify-center">
      <View className="m-4 flex-row items-center justify-center">
        <TouchableOpacity onPress={() => router.back()} className="">
          <Ionicons name="chevron-back" size={28} color="#333" />
        </TouchableOpacity>
        <View className="m-4 flex-1  justify-center">
          <Text className="text-gray-700 text-4xl font-bold">Welcome!</Text>
          <Text className="text-gray-500 text-md ">Let's get you set up.</Text>
        </View>
      </View>
      <TouchableOpacity onPress={()=>router.push("/signup/create-home")}>
        <View className="flex-row items-center m-4 p-4 border-2 border-gray-200 rounded-2xl">
          <Ionicons
            name="add-circle-outline"
            size={28}
            color="#A3B18A"
            style={{ marginRight: 10 }}
          />

          <View className="m-4 flex-1  justify-center">
            <Text className="text-gray-600 text-lg font-bold">
              Create a New Home
            </Text>
            <Text className="text-gray-400 text-md ">
              Start fresh with your roommates
            </Text>
          </View>
        </View>
      </TouchableOpacity>
      <TouchableOpacity onPress={()=>router.push("/signup/join-home")}>
        <View className="flex-row items-center m-4 p-4 border-2 border-gray-200 rounded-2xl">
          <MaterialCommunityIcons
            name="account-multiple-plus-outline"
            size={28}
            color="#A3B18A"
            style={{ marginRight: 10 }}
          />

          <View className="m-4 flex-1  justify-center">
            <Text className="text-gray-600 text-lg font-bold">
              Join an Existing Home
            </Text>
            <Text className="text-gray-400 text-md ">
              Use an invite code from a roommate
            </Text>
          </View>
        </View>
      </TouchableOpacity>
    </View>
  );
}

export default HomeSetup;
