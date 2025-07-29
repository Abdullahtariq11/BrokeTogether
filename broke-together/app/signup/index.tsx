import HeroSection from "@/components/UI/signup/HeroSection";
import "../../global.css";
import React from "react";
import { ScrollView, Text, View } from "react-native";

function index() {
  return (
    <ScrollView className="flex-1 bg-white ">
      {/*Header */}
      <HeroSection/>
      {/*SOcial button */}
      {/*Divider */}
      {/*Progress step */}
      {/*Form field */}
      {/*Continue Button */}
      {/*Footer */}
      <Text>Hello</Text>
    </ScrollView>
  );
}

export default index;
