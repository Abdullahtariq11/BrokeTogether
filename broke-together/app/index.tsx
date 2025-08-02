import { ActivityIndicator, Image, Text, View } from "react-native";
import "../global.css";
import { MotiView, MotiText } from "moti";
import ProgressBar from "@/components/UI/ProgressBar";
import { useRouter } from "expo-router";
import { useEffect } from "react";


export default function Index() {

  const router= useRouter();

  useEffect(()=>{
    const timer =setTimeout(() => {
      router.replace("/(app)/dashboard"); //change back to sigup
    }, 20);
    return ()=> clearTimeout(timer);
  },[]);
  return (
    <View className="flex-1 justify-center items-center bg-[#0B0F1A] px-6">
      {/* Logo Container */}
      <View className="w-20 h-20 rounded-2xl bg-white items-center justify-center shadow-lg mb-6">
        <MotiView
          from={{ opacity: 0, scale: 0.5 }}
          animate={{ opacity: 1, scale: 1 }}
          transition={{ type: "spring", duration: 1500 }}
          className="w-20 h-20 rounded-2xl  items-center justify-center shadow-lg mb-6"
        >
          <Image
            source={require("../assets/images/logo.png")}
            style={{ width: 100, height: 100, resizeMode: "contain" }}
          />
        </MotiView>
      </View>
      <MotiText
        from={{ opacity: 0, translateY: 20 }}
        animate={{ opacity: 1, translateY: 0 }}
        transition={{ delay: 0.7, duration: 1900 }}
        className="text-4xl font-extrabold text-white mb-2"
      >
        <Text className="text-4xl font-extrabold text-white mb-2">
          Broke Together
        </Text>
      </MotiText>
      {/* Animated Subtitle */}
      <MotiText
        from={{ opacity: 0 }}
        animate={{ opacity: 1 }}
        transition={{ delay: 0.8, duration: 1900 }}
        className="text-lg text-gray-300 mb-4"
      >
        <Text className="text-lg text-gray-300 mb-4">
          We are all broke but tracking it.
        </Text>
      </MotiText>
      <Text className="text-sm text-gray-400 mb-10">
        Smart budgeting • Secure • Simple
      </Text>
      <ProgressBar duration={5000} />
      <Text className="text-gray-400 mt-4 text-sm">
        Preparing your experience
      </Text>
    </View>
  );
}
