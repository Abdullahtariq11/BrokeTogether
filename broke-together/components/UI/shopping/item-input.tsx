import { Ionicons } from "@expo/vector-icons";
import React, { useState } from "react";
import { TextInput, TouchableOpacity, View } from "react-native";

type ItemInputProps = {
  setBuyList: React.Dispatch<React.SetStateAction<{ id: string; name: string; selected: boolean }[]>>;
  butList: { id: string; name: string; selected: boolean }[];
};

function ItemInput({ setBuyList, butList }: ItemInputProps) {
  const [itemName, setItemName] = useState<string>("");

  const addItem = (): void => {
    if (!itemName.trim()) return; // prevent empty adds
    const newItem = {
      id: Date.now().toString(),
      name: itemName.trim(),
      selected: false,
    };
    setBuyList([...butList, newItem]);
    setItemName(""); // clear input
  };

  return (
    <View className="flex-row m-2 items-center mb-4">
      <TextInput
        className="flex-1 bg-white border border-gray-300 rounded-lg p-3"
        placeholder="+ Add an item..."
        value={itemName}
        onChangeText={setItemName}
      />
      <TouchableOpacity onPress={addItem} className="ml-2 p-3 bg-[#A3B18A] rounded-lg">
        <Ionicons name="add" size={20} color="#fff" />
      </TouchableOpacity>
    </View>
  );
}

export default ItemInput;