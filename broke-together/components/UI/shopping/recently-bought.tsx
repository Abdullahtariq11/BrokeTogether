import { Ionicons } from "@expo/vector-icons";
import React, { useState } from "react";
import { Text, TouchableOpacity, View, FlatList } from "react-native";

type RecentlyBoughtProps = {
  items: { id: string; name: string; paidBy?: string; amount?: number }[];
};

const RecentlyBought: React.FC<RecentlyBoughtProps> = ({ items }) => {
  const [expanded, setExpanded] = useState(false);

  return (
    <View className="m-4">
      {/* Header */}
      <TouchableOpacity
        onPress={() => setExpanded(!expanded)}
        className="flex-row items-center justify-between bg-white rounded-xl px-4 py-3 shadow-sm border border-gray-200"
      >
        <Text className="text-gray-800 font-semibold text-lg">
          Recently Bought
        </Text>
        <View className="flex-row items-center">
          <Text className="text-gray-500 text-sm mr-2">
            {items.length} items
          </Text>
          <Ionicons
            name={expanded ? "chevron-up" : "chevron-down"}
            size={20}
            color="#555"
          />
        </View>
      </TouchableOpacity>

      {/* Collapsible Content */}
      {expanded && (
        <View className="mt-3">
          {items.length > 0 ? (
            <FlatList
              data={items}
              keyExtractor={(item) => item.id}
              renderItem={({ item }) => (
                <View className="flex-row justify-between items-center bg-gray-50 p-4 rounded-lg mb-2 border border-gray-200">
                  <View className="flex-col">
                    <View className="flex-row items-center gap-3 mb-1">
                      <Ionicons
                        name="checkmark-circle"
                        size={22}
                        color="#A3B18A"
                      />
                      <Text className="text-gray-800 text-base font-medium">
                        {item.name}
                      </Text>
                    </View>
                    {item.paidBy && (
                      <Text className="text-gray-500 text-sm">
                        Paid by {item.paidBy}
                      </Text>
                    )}
                  </View>
                  {item.amount && (
                    <Text className="text-gray-700 text-base font-semibold">
                      ${item.amount.toFixed(2)}
                    </Text>
                  )}
                </View>
              )}
            />
          ) : (
            <View className="bg-gray-50 p-6 rounded-lg border border-gray-200">
              <Text className="text-center text-gray-500 text-sm">
                No recent purchases yet.
              </Text>
            </View>
          )}
        </View>
      )}
    </View>
  );
};

export default RecentlyBought;