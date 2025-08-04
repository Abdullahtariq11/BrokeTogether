import { View, Text } from "react-native";

export default function ShoppingList() {
  return (
    <View className="flex-1 justify-center items-center bg-white">
      <Text className="text-2xl font-bold">Shopping List</Text>
      <Text className="text-gray-500 mt-2">Manage your shared shopping items.</Text>
    </View>
  );
}