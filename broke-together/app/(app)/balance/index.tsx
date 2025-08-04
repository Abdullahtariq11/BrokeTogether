import BalanceCard from "@/components/UI/balancePot/balance-card";
import Info from "@/components/UI/balancePot/info";
import PotHeader from "@/components/UI/balancePot/pot-header";
import React, { useState } from "react";
import { ScrollView, Text, View, Alert } from "react-native";

function Balance() {
  const [balancingData, setBalancingData] = useState<
    { payee: string; paidTo: string; amount: number }[]
  >([
    { payee: "Abdullah", paidTo: "Sameen", amount: 40 },
    { payee: "Mustafa", paidTo: "Abdullah", amount: 30 },
  ]);

  const handleMarkReceived = (index: number) => {
    Alert.alert(
      "Confirm Payment",
      "Are you sure you received this payment?",
      [
        { text: "Cancel", style: "cancel" },
        {
          text: "Yes",
          style: "default",
          onPress: () => {
            const updatedData = [...balancingData];
            updatedData.splice(index, 1); // Remove the transaction
            setBalancingData(updatedData);
          },
        },
      ]
    );
  };

  return (
    <View className="flex-1 bg-[#F8F9F6]">
      {/* Header */}
      <PotHeader />

      <ScrollView
        className="flex-1"
        contentContainerStyle={{ padding: 16, paddingBottom: 40 }}
        showsVerticalScrollIndicator={false}
      >
        {/* Info Section */}
        <Info />

        {/* Settlement Cards or All Clear State */}
        {balancingData.length > 0 ? (
          balancingData.map((data, index) => (
            <BalanceCard
              key={index}
              data={data}
              isPayee={true}
              onMarkReceived={() => handleMarkReceived(index)}
            />
          ))
        ) : (
          <View className="items-center justify-center mt-20">
            <Text className="text-2xl font-bold text-gray-700 mb-2">
              You’re all balanced!
            </Text>
            <Text className="text-gray-500 text-center px-10">
              Harmony achieved. There are no pending settlements.
            </Text>
          </View>
        )}
      </ScrollView>
    </View>
  );
}

export default Balance;