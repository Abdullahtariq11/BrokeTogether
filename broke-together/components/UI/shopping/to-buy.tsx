import { Ionicons } from "@expo/vector-icons";
import React, { useState } from "react";
import { Text, TextInput, TouchableOpacity, View } from "react-native";

type ToBuyProps = {
  item: { id: string; name: string; selected: boolean };
  onSave: (id: string, newName: string) => void;
  onDelete: (id: string) => void;
  onComplete: (id: string) => void;
};

const ToBuy: React.FC<ToBuyProps> = ({ item, onSave, onDelete, onComplete }) => {
  const [edit, setIsEdit] = useState<boolean>(false);
  const [newName, setNewName] = useState(item.name);

  return (
    <View
      key={item.id}
      className="flex-row items-center justify-between bg-white p-3 rounded-lg mb-2 border border-gray-200"
    >
      {/* Left: Checkbox + Item Name */}
      <View className="flex-row items-center gap-3">
        <TouchableOpacity onPress={() => onComplete(item.id)}>
          <Ionicons
            name={item.selected ? "checkbox" : "square-outline"}
            size={22}
            color={item.selected ? "#A3B18A" : "gray"}
          />
        </TouchableOpacity>

        {!edit ? (
          <Text className="text-gray-700">{item.name}</Text>
        ) : (
          <TextInput
            className="border-b border-gray-300 text-gray-700 flex-1"
            value={newName}
            onChangeText={setNewName}
          />
        )}
      </View>

      {/* Right: Actions */}
      <View className="flex-row gap-2">
        {!edit ? (
          <>
            <TouchableOpacity onPress={() => setIsEdit(true)}>
              <Ionicons name="pencil" size={18} color="gray" />
            </TouchableOpacity>
            <TouchableOpacity onPress={() => onDelete(item.id)}>
              <Ionicons name="trash-bin" size={18} color="red" />
            </TouchableOpacity>
          </>
        ) : (
          <>
            <TouchableOpacity
              onPress={() => {
                onSave(item.id, newName);
                setIsEdit(false);
              }}
            >
              <Text className="text-green-600">Save</Text>
            </TouchableOpacity>
            <TouchableOpacity onPress={() => setIsEdit(false)}>
              <Text className="text-gray-500">Cancel</Text>
            </TouchableOpacity>
          </>
        )}
      </View>
    </View>
  );
};

export default ToBuy;