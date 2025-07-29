import React from "react";
import { Text, TouchableOpacity, View } from "react-native";
import { AntDesign, FontAwesome } from "@expo/vector-icons";

function SocialButton() {
  return (
    <View className="w-full flex-1 gap-1 pb-1 px-6 mt-6 space-y-4 items-center">
      <Text className="text-lg text-black font-semibold text-center">
        Create Your Account
      </Text>
      <Text className="text-sm text-black pb-5 text-center">
        Start your journey to financial freedom
      </Text>

      {/* Google Button */}
      <TouchableOpacity className="w-full flex-row items-center justify-center border border-gray-300 p-4 rounded-lg bg-white">
        <AntDesign
          name="google"
          size={20}
          color="#DB4437"
          style={{ marginRight: 8 }}
        />
        <Text className="text-gray-800 font-medium">Continue with Google</Text>
      </TouchableOpacity>

      {/* Apple Button */}
      <TouchableOpacity className="w-full flex-row items-center justify-center border border-gray-300 p-4 rounded-lg bg-black">
        <FontAwesome
          name="apple"
          size={22}
          color="#fff"
          style={{ marginRight: 8 }}
        />
        <Text className="text-white font-medium">Continue with Apple</Text>
      </TouchableOpacity>
    </View>
  );
}

export default SocialButton;