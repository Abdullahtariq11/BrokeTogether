import React, { useState } from "react";
import {
  Alert,
  Image,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from "react-native";
import { Ionicons } from "@expo/vector-icons";
import * as ImagePicker from "expo-image-picker";

function Profile() {
  const [imagePicker, setImagePicker] = useState<string | null>(null);
  const [isEdit, setIsEdit] = useState<boolean>(false);
  const [profileData, setProfileData] = useState<{
    Name: string;
    Email: string;
  }>({
    Name: "Abdullah Tariq",
    Email: "abdullah@email.com",
  });

  // Image Picker
  const pickImage = async () => {
    const permission = await ImagePicker.requestMediaLibraryPermissionsAsync();
    if (!permission.granted) {
      Alert.alert("Permission is required to access gallery");
      return;
    }
    const result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true,
      aspect: [1, 1],
      quality: 0.7,
    });
    if (!result.canceled) {
      setImagePicker(result.assets[0].uri);
    }
  };

  const handleChange = (field: string, value: string) => {
    setProfileData({ ...profileData, [field]: value });
  };

  const handleSave = () => {
    Alert.alert("Profile updated successfully!");
    setIsEdit(false);
  };

  return (
    <View className="bg-white p-6 rounded-2xl shadow-md mt-4 mb-6">
      <Text className="text-xl font-bold text-gray-800 mb-4">Profile</Text>

      <View className="items-center mb-5">
        {/* Profile Image */}
        <View className="relative">
          <Image
            source={
              imagePicker
                ? { uri: imagePicker }
                : require("@/assets/images/default-user.png")
            }
            className="w-28 h-28 rounded-full border-4 border-gray-200"
          />
          <TouchableOpacity
            onPress={pickImage}
            className="absolute bottom-0 right-0 bg-[#A3B18A] p-2 rounded-full shadow-md"
          >
            <Ionicons name="camera" size={18} color="white" />
          </TouchableOpacity>
        </View>
      </View>

      {/* Editable / View Mode */}
      {!isEdit ? (
        <View className="items-center">
          <Text className="text-lg font-semibold text-gray-700">
            {profileData.Name}
          </Text>
          <Text className="text-base text-gray-500">{profileData.Email}</Text>

          <TouchableOpacity
            onPress={() => setIsEdit(true)}
            className="flex-row items-center mt-4 border border-gray-300 px-4 py-2 rounded-full"
          >
            <Ionicons name="pencil" size={18} color="#333" />
            <Text className="ml-2 text-gray-700 font-medium">Edit Profile</Text>
          </TouchableOpacity>
        </View>
      ) : (
        <View className="mt-4 space-y-3">
          <TextInput
            className="text-base rounded-lg p-3  text-black bg-gray-100 border border-gray-300"
            placeholder="Full Name"
            value={profileData.Name}
            onChangeText={(value) => handleChange("Name", value)}
          />
          <TextInput
            className="text-base rounded-lg mt-2 p-3 text-black bg-gray-100 border border-gray-300"
            placeholder="Email"
            keyboardType="email-address"
            value={profileData.Email}
            onChangeText={(value) => handleChange("Email", value)}
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
    </View>
  );
}

export default Profile;