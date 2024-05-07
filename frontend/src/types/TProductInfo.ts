import componentTypes from 'enums/componentTypes.ts';

type TProductInfo = {
  productId: number;
  brand: string;
  model: string;
  price: number;
  country: string;
  description: string;
  image: string;
  amount: number;
  power: number;
  likes: number;
  productType: keyof typeof componentTypes;
};

export default TProductInfo;
