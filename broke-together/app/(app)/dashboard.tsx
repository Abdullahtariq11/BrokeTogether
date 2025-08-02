import ChippedCard from "@/components/UI/dashboard/chipped-card";
import DashboardHeader from "@/components/UI/dashboard/dashboard-header";
import RecentTransaction from "@/components/UI/dashboard/recent-transaction";
import { View, Text, TouchableOpacity } from "react-native";
import { Ionicons } from "@expo/vector-icons";

export default function Dashboard() {
  return (
    <View className="flex-1 bg-[#F8F9F6]">
      {/* Header */}
      <DashboardHeader />

      {/* Who's Chipped In Card */}
      <ChippedCard />

      {/* Balance Button */}
      <TouchableOpacity className="mx-4">
        <View className="flex p-4 border border-[#A3B18A] rounded-xl justify-center items-center bg-[#A3B18A20]">
          <Text className="text-lg font-semibold text-[#6B705C]">
            $ Balance the Pot
          </Text>
        </View>
      </TouchableOpacity>

      {/* Recent Transactions */}
      <RecentTransaction />

      {/* Floating Add Button */}
      <TouchableOpacity className="absolute bottom-20 right-8 bg-[#E98074] p-4 rounded-full shadow-lg">
        <Ionicons name="add" size={28} color="#fff" />
      </TouchableOpacity>
    </View>
  );
}