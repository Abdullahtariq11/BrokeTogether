import React from "react";
import { View, Text } from "react-native";

function RecentTransaction() {
  const transactions = [
    { name: "Pizza", amount: 45.25, description: "Paid by Abdullah on 21st June" },
    { name: "Burger", amount: 20.25, description: "Paid by Sameen on 21st June" },
    { name: "Grocery", amount: 10.25, description: "Paid by Mustafa on 21st June" },
  ];
  return (
    <View
      className="m-4 p-5 rounded-2xl bg-white border border-gray-100 shadow-md"
      style={{
        shadowColor: "#000",
        shadowOpacity: 0.05,
        shadowOffset: { width: 0, height: 3 },
        shadowRadius: 5,
        elevation: 2,
      }}
    >
      <Text className="text-xl font-extrabold text-gray-700 mb-4">
        Recent Transactions
      </Text>
      {transactions.map((transaction, index) => (
        <View
          key={index}
          className="flex-row justify-between items-center py-3 border-b border-gray-100 last:border-b-0"
        >
          <View>
            <Text className="text-lg font-semibold text-gray-700">{transaction.name}</Text>
            <Text className="text-sm text-gray-500">{transaction.description}</Text>
          </View>
          <Text className="text-base font-bold text-gray-800">${transaction.amount.toFixed(2)}</Text>
        </View>
      ))}
    </View>
  );
}

export default RecentTransaction;