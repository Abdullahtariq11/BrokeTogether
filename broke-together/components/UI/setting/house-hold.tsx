import { Ionicons } from "@expo/vector-icons";
import React, { useState } from "react";
import { View, Text, TouchableOpacity, TextInput, Alert } from "react-native";

function HouseHold() {
  const houseInfo: { houseName: string; members: number; inviteCode: string } =
    {
      houseName: "820 Duncan Ave",
      members: 3,
      inviteCode: "AB6X7D",
    };
  const members: { name: string; amount: number }[] = [
    { name: "Abdullah", amount: 45.25 },
    { name: "Mustafa", amount: -20.25 },
    { name: "Sameen", amount: -10.25 },
  ];

  const [isEdit, setIsEdit] = useState<boolean>(false);
  const [profileData, setProfileData] = useState<{
    houseName: string;
    members: number;
    inviteCode: string;
  }>(houseInfo);
  const handleChange = (field: string, value: string) => {
    setProfileData({ ...profileData, [field]: value });
  };

  const handleSave = () => {
    Alert.alert("Profile updated successfully!");
    setIsEdit(false);
  };

  return (
    <View className="bg-white p-6 rounded-2xl shadow-md mt-4 mb-6">
      <Text className="text-xl font-bold text-gray-800 mb-4">Household</Text>
      {/* Editable / View Mode */}
      {!isEdit ? (
        <View className="flex-row align-middle justify-between gap-3 items-center">
          <View className="flex ">
            <Text className="text-lg font-semibold text-gray-700">
              {profileData.houseName}
            </Text>
            <Text className="text-sm  text-gray-400">
              {profileData.members} members
            </Text>
          </View>

          <TouchableOpacity
            onPress={() => setIsEdit(true)}
            className="flex-row items-center  border border-gray-300 px-4 py-2 rounded-full"
          >
            <Ionicons name="pencil" size={18} color="#333" />
            <Text className="ml-2 text-gray-700 font-medium">Edit Name</Text>
          </TouchableOpacity>
        </View>
      ) : (
        <View className="mt-4 space-y-3">
          <TextInput
            className="text-base rounded-lg p-3  text-black bg-gray-100 border border-gray-300"
            placeholder="House Name"
            value={profileData.houseName}
            onChangeText={(value) => handleChange("houseName", value)}
          />

          <View className="flex-row justify-between mt-4">
            <TouchableOpacity
              onPress={handleSave}
              className="flex-1 bg-[#A3B18A] py-3 rounded-lg mr-2"
            >
              <Text className="text-center text-white font-semibold text-base">
                Save
              </Text>
            </TouchableOpacity>
            <TouchableOpacity
              onPress={() => setIsEdit(false)}
              className="flex-1 border border-gray-300 py-3 rounded-lg ml-2"
            >
              <Text className="text-center text-gray-700 font-semibold text-base">
                Cancel
              </Text>
            </TouchableOpacity>
          </View>
        </View>
      )}
      <View className="flex-1 mt-4 h-px bg-gray-300" />
      <View className="flex-1 h-px bg-gray-300" />
      <Text className="text-lg font-bold text-gray-800 mt-4 mb-4">
        Invite Code
      </Text>
      <View className="flex-row align-middle justify-between bg-[#a3b18a31] py-3 rounded-lg mr-2">
        <Text className="items-center pl-3 text-center">
          {profileData.inviteCode}
        </Text>
        <TouchableOpacity>
          <Ionicons className="pr-3" name="copy" size={18} color="#333" />
        </TouchableOpacity>
      </View>
      <TouchableOpacity>
        <Text>Share code</Text>
      </TouchableOpacity>
    </View>
  );
}

export default HouseHold;
