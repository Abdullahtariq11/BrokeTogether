import React from "react";
import { Text, View } from "react-native";

function ChippedCard() {
  const members = [
    { name: "Abdullah", amount: 45.25 },
    { name: "Mustafa", amount: -20.25 },
    { name: "Sameen", amount: -10.25 },
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
        Who's Chipped In
      </Text>

      {members.map((member, index) => (
        <View
          key={index}
          className="flex-row justify-between items-center py-3 border-b border-gray-100 last:border-b-0"
        >
          <Text className="text-base text-gray-700">{member.name}</Text>
          <Text
            className={`text-base font-semibold ${
              member.amount >= 0 ? "text-green-500" : "text-red-400"
            }`}
          >
            {member.amount >= 0 ? "+" : ""}
            ${Math.abs(member.amount).toFixed(2)}
          </Text>
        </View>
      ))}
    </View>
  );
}

export default ChippedCard;