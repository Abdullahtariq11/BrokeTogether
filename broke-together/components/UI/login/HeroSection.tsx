import React, { useEffect, useState } from "react";
import { Image, Text, View } from "react-native";
import { Herodata } from "../../../lib/types";
import { MotiView, MotiText } from "moti";

function HeroSection() {
  return (
    <View className="bg-slate-900 flex-1 pt-20 items-center">
      {/* Logo Container */}
      <View className="w-20 h-20 rounded-2xl bg-white items-center justify-center shadow-lg mb-6">
        <MotiView
          from={{ opacity: 0, scale: 0.5 }}
          animate={{ opacity: 1, scale: 1 }}
          transition={{ type: "spring", duration: 1500 }}
          className="w-20 h-20 rounded-2xl  items-center justify-center shadow-lg mb-6"
        >
          <Image
            source={require("../../../assets/images/logo.png")}
            style={{ width: 100, height: 100, resizeMode: "contain" }}
          />
        </MotiView>
      </View>
      <Text className="text-white text-2xl pt-2 font-bold">
        Broke Together
      </Text>
      <Text className="text-white py-2 text-sm">
        Your Personal finance companion
      </Text>
    </View>
  );
}

export default HeroSection;
