import ChippedCard from "@/components/UI/dashboard/chipped-card";
import DashboardHeader from "@/components/UI/dashboard/dashboard-header";
import RecentTransaction from "@/components/UI/dashboard/recent-transaction";
import { View, Text, TouchableOpacity, ScrollView } from "react-native";
import { Ionicons } from "@expo/vector-icons";
import { useState } from "react";
import ContributionModal from "@/components/UI/dashboard/add-contribution-modal";
import { router } from "expo-router";

export default function Dashboard() {
  const [isModalVisible,setModalVisible]=useState<boolean>(false);
  return (
    <View className="flex-1 bg-[#F8F9F6]">
      <DashboardHeader />

      <ScrollView
        className="flex-1 bg-[#F8F9F6]"
        contentContainerStyle={{ paddingBottom: 100 }}
      >
        <ChippedCard />

        {/* Balance the Pot - Full Width CTA */}
        <TouchableOpacity onPress={()=>router.push("/(app)/balance")} className="mx-4 mt-2">
          <View className="w-full py-4 rounded-xl bg-[#A3B18A]">
            <Text className="text-lg font-semibold text-white text-center">
              Balance the Pot
            </Text>
          </View>
        </TouchableOpacity>

        <RecentTransaction />
      </ScrollView>

      {/* Floating Add Button */}
      <TouchableOpacity
      onPress={()=>setModalVisible(true)}
        className="absolute bottom-24 right-6 rounded-full shadow-lg"
        style={{
          backgroundColor: "#E98074",
          padding: 14,
          elevation: 6,
          shadowColor: "#000",
          shadowOpacity: 0.2,
          shadowOffset: { width: 0, height: 3 },
          shadowRadius: 6,
        }}
      >
        <Ionicons name="add" size={28} color="#fff" />
      </TouchableOpacity>

      <ContributionModal visible={isModalVisible} onClose={() => setModalVisible(false)}/>
    </View>
  );
}