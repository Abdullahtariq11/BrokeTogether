import HouseHold from '@/components/UI/setting/house-hold';
import Profile from '@/components/UI/setting/profile';
import SettingHeader from '@/components/UI/setting/setting-header';
import React from 'react'
import { ScrollView, View } from 'react-native'

function Settings() {
  return (
    <View className='flex-1'>
        <SettingHeader/>
        <ScrollView className='flex-1 bg-[#F8F9F6] px-4'>
          {/* Profile Section */}
          <Profile/>
          {/* Household Section */}
          <HouseHold/>
          {/* Members List */}
          {/* Log out Button */}

        </ScrollView>
    </View>
  )
}

export default Settings;