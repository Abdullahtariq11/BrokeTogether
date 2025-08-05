// import AddContributionModal from "@/components/UI/balancePot/add-contribution-modal";
import AddContributionModal from "@/components/UI/shopping/add-contribution-modal";
import DashboardHeader from "@/components/UI/dashboard/dashboard-header";
import ItemInput from "@/components/UI/shopping/item-input";
import RecentlyBought from "@/components/UI/shopping/recently-bought";
import ToBuy from "@/components/UI/shopping/to-buy";
import { useState } from "react";
import { View, Text, ScrollView } from "react-native";

export default function ShoppingList() {
  const [butList, setBuyList] = useState<
    { id: string; name: string; selected: boolean }[]
  >([
    { id: "1", name: "Milk", selected: false },
    { id: "2", name: "Bread", selected: false },
  ]);

  const [recentlyBought, setRecentlyBought] = useState<
    { id: string; name: string; paidBy:string; amount?: number }[]
  >([]);

  const [visible, setVisible] = useState<boolean>(false);
  const [selectedItem, setSelectedItem] = useState<string | null>(null);

  const handleComplete = (id: string) => {
    setSelectedItem(id);
    setVisible(true);
  };

  const handleAddExpense = (amount: number, paidBy: string) => {
    const item = butList.find((i) => i.id === selectedItem);
    if (item) {
      setRecentlyBought((prev) => [
        ...prev,
        { id: item.id, name: item.name,paidBy, amount },
      ]);
      setBuyList((prev) => prev.filter((i) => i.id !== item.id));
    }
    setVisible(false);
    setSelectedItem(null);
  };

  return (
    <View className="flex-1 gap-3 bg-[#F8F9F6]">
      <DashboardHeader />
      <ItemInput setBuyList={setBuyList} butList={butList} />
      <View className="flex-row justify-between items-center m-4">
        <Text className="text-2xl font-bold text-center">To Buy</Text>
        <Text className="text-center bg-gray-100 p-2 rounded-2xl">
          {butList.length} items
        </Text>
      </View>

      <ScrollView className="p-2">
        {butList.map((item) => (
          <ToBuy
            key={item.id}
            item={item}
            onSave={(id, newName) =>
              setBuyList((prev) =>
                prev.map((i) => (i.id === id ? { ...i, name: newName } : i))
              )
            }
            onDelete={(id) =>
              setBuyList((prev) => prev.filter((i) => i.id !== id))
            }
            onComplete={handleComplete}
          />
        ))}
      </ScrollView>

      <RecentlyBought items={recentlyBought} />

      {/* Expense Modal */}
      {selectedItem && (
        <AddContributionModal
          visible={visible}
          onClose={() => setVisible(false)}
          onAdd={(amount, paidBy) => handleAddExpense(amount, paidBy)}
        />
      )}
    </View>
  );
}