import React, { useEffect, useState } from "react";
import { Image, Text, View } from "react-native";
import { Herodata } from "../../../lib/types";
import { MotiView, MotiText } from "moti";

function HeroSection() {
  const data: Herodata[] = [
    {
      id: 1,
      title: "Take control of your financial future",
      description: "Track, Budget and Grow",
    },
    {
      id: 2,
      title: "Smart budgeting made simple",
      description: "Automated Recomendation and Personalisation",
    },
    {
      id: 3,
      title: "Achieve your financial goals",
      description: "Take control of your finances",
    },
  ];

  const backgroundColors: string[] = ["#0B0F1A", "#6D28D9", "#F97316"]; // Dark, purple, orange

  const [currentTextIndex, setCurrentTextIndex] = useState(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentTextIndex((prev) => (prev + 1) % data.length);
    }, 3000); // changes every 1 second
    return () => clearInterval(interval);
  }, []);
  return (
    <MotiView
      from={{ backgroundColor: backgroundColors[currentTextIndex] }}
      animate={{ backgroundColor: backgroundColors[currentTextIndex] }}
      transition={{ type: "timing", duration: 500 }}
      className="flex-1 py-10 items-center"
    >
      <Text className="text-white text-2xl pt-10 font-bold">
        Broke Together
      </Text>
      <Text className="text-white py-2 text-sm">
        Your Personal finance companion
      </Text>
      <MotiText
        key={currentTextIndex} // ensures re-render
        from={{ opacity: 0, translateY: 20 }}
        animate={{ opacity: 1, translateY: 0 }}
        transition={{ type: "timing", duration: 400 }}
        className="text-lg font-bold text-white mt-1"
      >
        {data[currentTextIndex].title}
      </MotiText>
      <MotiText
        key={data[currentTextIndex].description} // ensures re-render
        from={{ opacity: 0, translateY: 20 }}
        animate={{ opacity: 1, translateY: 0 }}
        transition={{ type: "timing", duration: 400 }}
        className="text-sm text-white mt-1"
      >
        {data[currentTextIndex].description}
      </MotiText>
      {/* Dot Indicators */}
      <View className="flex-row mt-3 gap-1 space-x-3">
        {data.map((_, index) => (
          <View
            key={index}
            className={`w-2.5 h-2.5 rounded-full ${
              index === currentTextIndex ? "bg-purple-600" : "bg-gray-300"
            }`}
          />
        ))}
      </View>
    </MotiView>
  );
}

export default HeroSection;
