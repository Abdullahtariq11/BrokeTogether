import React, { useState } from "react";
import { Modal, View, Text, TouchableOpacity, TextInput } from "react-native";
import Ionicons from "@expo/vector-icons/Ionicons";

type AddContributionModalProps = {
  visible: boolean;
  onClose: () => void;
  onAdd: (amount: number, paidBy: string) => void;
};

function AddContributionModal({
  visible,
  onClose,
  onAdd,
}: AddContributionModalProps) {
  const [paidBy, setPaidBy] = useState<string | null>(null);
  const [amount, setAmount] = useState<string>("");

  const members = [
    { name: "Abdullah", initial: "A" },
    { name: "Mustafa", initial: "M" },
    { name: "Sameen", initial: "S" },
  ];

  const handleAdd = () => {
    if (amount && paidBy) {
      onAdd(parseFloat(amount), paidBy);
    }
  };
  return (
    <Modal visible={visible} animationType="slide" transparent>
      <View className="flex-1 justify-end bg-black/50">
        <View className="m-2 p-6 rounded-t-3xl bg-white">
          {/* Header */}
          <View className="flex-row justify-between items-center mb-6">
            <Text className="text-lg font-bold text-gray-800">
              Add Contribution
            </Text>
            <TouchableOpacity onPress={onClose}>
              <Ionicons name="close" size={26} color="#555" />
            </TouchableOpacity>
          </View>

          {/* Amount Input */}
          <Text className="font-bold text-md text-gray-700 mb-2">
            How much did it cost?
          </Text>
          <TextInput
            keyboardType="numeric"
            value={amount}
            onChangeText={setAmount}
            placeholder="$ 0.00"
            className="text-3xl font-bold text-gray-800 border-b border-gray-300 mb-6 p-2"
          />

          {/* Paid By */}
          <Text className="font-bold text-md text-gray-700 mb-4">
            Who paid for this?
          </Text>
          <View className="flex gap-3 mb-6">
            {members.map((u, idx) => (
              <TouchableOpacity
                key={idx}
                onPress={() => setPaidBy(u.initial)}
                className={`rounded-lg p-3 ${
                  paidBy === u.initial
                    ? "border-2 border-[#A3B18A] bg-[#f1f4ed]"
                    : "border border-gray-200 bg-[#f8f8f8]"
                }`}
              >
                <Text
                  className={`text-lg ${
                    paidBy === u.initial
                      ? "text-[#6B705C] font-semibold"
                      : "text-gray-700"
                  }`}
                >
                  {u.name}
                </Text>
              </TouchableOpacity>
            ))}
          </View>

          {/* Buttons */}
          <View className="flex-row gap-4 mt-2">
            <TouchableOpacity
              onPress={onClose}
              className="flex-1 bg-white border border-gray-300 p-4 rounded-lg"
            >
              <Text className="text-center text-gray-600 font-medium">
                Cancel
              </Text>
            </TouchableOpacity>
            <TouchableOpacity
              onPress={handleAdd}
              className="flex-1 bg-[#A3B18A] p-4 rounded-lg"
            >
              <Text className="text-center text-white font-semibold">
                Add Expense
              </Text>
            </TouchableOpacity>
          </View>
        </View>
      </View>
    </Modal>
  );24
}

export default AddContributionModal;
