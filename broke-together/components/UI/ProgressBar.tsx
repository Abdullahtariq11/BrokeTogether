import { useState } from "react";
import { View, LayoutChangeEvent } from "react-native";
import { MotiView } from "moti";

function ProgressBar({ duration = 5000 }) {
  const [barWidth, setBarWidth] = useState(0);

  const onLayout = (e: LayoutChangeEvent) => {
    setBarWidth(e.nativeEvent.layout.width);
  };

  return (
    <View
      className="w-64 h-2 bg-gray-700 rounded-full mt-6 overflow-hidden"
      onLayout={onLayout}
    >
      {barWidth > 0 && (
        <MotiView
          from={{ width: 0 }}
          animate={{ width: barWidth }}
          transition={{ type: "timing", duration }}
          className="h-2 bg-purple-600 rounded-full"
        />
      )}
    </View>
  );
}

export default ProgressBar;