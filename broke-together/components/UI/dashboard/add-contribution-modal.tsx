import React, { useState } from "react";
import {
  Modal,
  ScrollView,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from "react-native";
import { Ionicons } from "@expo/vector-icons";

interface ContributionModalProps {
  visible: boolean;
  onClose: () => void;
}

const ContributionModal: React.FC<ContributionModalProps> = ({
  visible,
  onClose,
}) => {
  const members = [
    { name: "Abdullah", initial: "A" },
    { name: "Mustafa", initial: "M" },
    { name: "Sameen", initial: "S" },
  ];

  const [description, setDescription] = useState<string>("");
  const [amount, setAmount] = useState<string>("");
  const [paidBy, setPaidBy] = useState<string | null>(null);
  const [splitBetween, setSplitBetween] = useState<string[]>([]);

  // Toggle split between selection
  const handleSplit = (initial: string) => {
    setSplitBetween((prev) =>
      prev.includes(initial) ? prev.filter((i) => i !== initial) : [...prev, initial]
    );
  };

  return (
    <Modal visible={visible} animationType="slide" transparent>
      <View className="flex-1 justify-end bg-black/50">
        <View className="bg-white rounded-t-3xl p-6 max-h-[80%]">
          {/* Header */}
          <View className="flex-row justify-between items-center mb-4">
            <Text className="text-lg font-semibold text-gray-800">
              Add Contribution
            </Text>
            <TouchableOpacity onPress={onClose}>
              <Ionicons name="close" size={24} color="#555" />
            </TouchableOpacity>
          </View>

          <ScrollView showsVerticalScrollIndicator={false}>
            {/* Amount */}
            <Text className="text-sm text-gray-500 mb-1">Amount</Text>
            <TextInput
              keyboardType="numeric"
              value={amount}
              onChangeText={setAmount}
              placeholder="$0.00"
              className="text-3xl font-bold text-gray-800 border-b border-gray-300 mb-6"
            />

            {/* Description */}
            <Text className="text-sm text-gray-500 mb-1">Description</Text>
            <TextInput
              value={description}
              onChangeText={setDescription}
              placeholder="e.g., Weekly Groceries"
              className="border border-gray-300 rounded-lg p-3 text-base mb-6"
            />

            {/* Paid by */}
            <Text className="text-sm text-gray-500 mb-3">Paid by</Text>
            <View className="flex-row gap-4 mb-6">
              {members.map((u, idx) => (
                <TouchableOpacity
                  key={idx}
                  onPress={() => setPaidBy(u.initial)}
                  className={`w-14 h-14 rounded-full justify-center items-center ${
                    paidBy === u.initial ? "bg-[#A3B18A]" : "bg-gray-200"
                  }`}
                >
                  <Text
                    className={`text-lg font-bold ${
                      paidBy === u.initial ? "text-white" : "text-gray-700"
                    }`}
                  >
                    {u.initial}
                  </Text>
                </TouchableOpacity>
              ))}
            </View>

            {/* Split between */}
            <Text className="text-sm text-gray-500 mb-3">Split between</Text>
            <View className="flex-row gap-4 mb-6">
              {members.map((u, idx) => (
                <TouchableOpacity
                  key={idx}
                  onPress={() => handleSplit(u.initial)}
                  className={`w-14 h-14 rounded-full justify-center items-center ${
                    splitBetween.includes(u.initial)
                      ? "bg-[#A3B18A]"
                      : "bg-gray-200"
                  }`}
                >
                  <Text
                    className={`text-lg font-bold ${
                      splitBetween.includes(u.initial) ? "text-white" : "text-gray-700"
                    }`}
                  >
                    {u.initial}
                  </Text>
                </TouchableOpacity>
              ))}
            </View>

            {/* Add Button */}
            <TouchableOpacity
              className="w-full bg-[#A3B18A] p-4 rounded-lg mt-4"
              onPress={() => {
                console.log({ amount, description, paidBy, splitBetween });
                onClose();
              }}
            >
              <Text className="text-center text-white font-semibold text-lg">
                Add to Pot
              </Text>
            </TouchableOpacity>
          </ScrollView>
        </View>
      </View>
    </Modal>
  );
};

export default ContributionModal;