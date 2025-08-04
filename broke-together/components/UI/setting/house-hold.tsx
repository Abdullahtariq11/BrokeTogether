import { Ionicons } from "@expo/vector-icons";
import React, { useState } from "react";
import {
  View,
  Text,
  TouchableOpacity,
  TextInput,
  Alert,
  ScrollView,
} from "react-native";

function HouseHold() {
  const houseInfo = {
    houseName: "820 Duncan Ave",
    members: 3,
    inviteCode: "AB6X7D",
  };
  const members = [{ name: "Abdullah" }, { name: "Mustafa" }, { name: "Sameen" }];

  const [isEdit, setIsEdit] = useState(false);
  const [profileData, setProfileData] = useState(houseInfo);

  const handleChange = (field: string, value: string) => {
    setProfileData({ ...profileData, [field]: value });
  };

  const handleSave = () => {
    Alert.alert("Household updated successfully!");
    setIsEdit(false);
  };

  return (
    <View className="bg-white p-6 rounded-2xl shadow-md mt-4 mb-6">
      {/* Header */}
      <Text className="text-xl font-bold text-gray-800 mb-4">Household</Text>

      {/* Editable / View Mode */}
      {!isEdit ? (
        <View className="flex-row justify-between items-center">
          <View>
            <Text className="text-lg font-semibold text-gray-700">
              {profileData.houseName}
            </Text>
            <Text className="text-sm text-gray-500">
              {profileData.members} members
            </Text>
          </View>

          <TouchableOpacity
            onPress={() => setIsEdit(true)}
            className="flex-row items-center border border-gray-300 px-4 py-2 rounded-full"
          >
            <Ionicons name="pencil" size={18} color="#333" />
            <Text className="ml-2 text-gray-700 font-medium">Edit Name</Text>
          </TouchableOpacity>
        </View>
      ) : (
        <View className="mt-4 space-y-3">
          <TextInput
            className="text-base rounded-lg p-3 bg-gray-100 border border-gray-300"
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

      {/* Divider */}
      <View className="h-px bg-gray-200 my-5" />

      {/* Invite Code Section */}
      <Text className="text-lg font-bold text-gray-800 mb-3">Invite Code</Text>
      <View className="flex-row items-center justify-between bg-[#a3b18a1a] py-3 px-4 rounded-lg">
        <Text className="text-lg font-bold text-gray-800">
          {profileData.inviteCode}
        </Text>
        <TouchableOpacity>
          <Ionicons name="copy" size={20} color="#333" />
        </TouchableOpacity>
      </View>
      <TouchableOpacity className="mt-3">
        <View className="p-3 rounded-lg border border-[#A3B18A] bg-[#a3b18a1a]">
          <Text className="text-center text-[#A3B18A] font-semibold">
            Share Invite Link
          </Text>
        </View>
      </TouchableOpacity>

      {/* Divider */}
      <View className="h-px bg-gray-200 my-5" />

      {/* Members Section */}
      <Text className="text-lg font-bold text-gray-800 mb-3">Members</Text>
      <View className="space-y-3">
        {members.map((member, index) => (
          <View
            key={index}
            className="flex-row justify-between items-center py-3 border-b border-gray-100"
          >
            <Text className="text-base text-gray-700">{member.name}</Text>
            <Ionicons name="person-outline" size={20} color="#555" />
          </View>
        ))}
      </View>
    </View>
  );
}

export default HouseHold;