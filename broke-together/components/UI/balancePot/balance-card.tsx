import React from "react";
import { Text, View, TouchableOpacity } from "react-native";
import { Ionicons } from "@expo/vector-icons";

type BalanceCardProps = {
  data: { payee: string; paidTo: string; amount: number };
  onMarkReceived?: () => void;
  isPayee?: boolean; // enable button only for payee
};

function BalanceCard({ data, onMarkReceived, isPayee }: BalanceCardProps) {
  return (
    <View
      className="m-4 p-5 rounded-2xl bg-white border border-gray-100 shadow-md"
      style={{
        shadowColor: "#000",
        shadowOpacity: 0.08,
        shadowOffset: { width: 0, height: 4 },
        shadowRadius: 6,
        elevation: 3,
      }}
    >
      {/* Header: From → To */}
      <View className="flex-row items-center justify-center mb-3">
        <Text className="text-lg font-semibold text-gray-700">{data.payee}</Text>
        <Ionicons name="arrow-forward" size={20} color="#A3B18A" className="mx-2" />
        <Text className="text-lg font-semibold text-gray-700">{data.paidTo}</Text>
      </View>

      {/* Amount */}
      <Text className="text-3xl font-extrabold text-center text-gray-800 mb-4">
        ${data.amount.toFixed(2)}
      </Text>

      {/* Mark as Received Button */}
      {isPayee && (
        <TouchableOpacity
          onPress={onMarkReceived}
          className="w-full bg-[#A3B18A] py-3 rounded-lg mt-2"
        >
          <Text className="text-center text-white font-semibold text-lg">
            Mark as Received
          </Text>
        </TouchableOpacity>
      )}
    </View>
  );
}

export default BalanceCard;