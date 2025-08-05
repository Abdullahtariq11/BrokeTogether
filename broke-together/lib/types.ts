export type Herodata = {
  id: number;
  title: string;
  description: string;
};

export type SocialButton = {
  icon: "";
  text: "";
};

export type ItemBought = {
  id: string;
  name: string;
  selected: boolean;
  boughtBy: string;
  amount: number;
};
