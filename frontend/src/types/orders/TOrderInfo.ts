import componentTypes from 'enums/componentTypes.ts';

type TOrderInfo = {
  orderId: number;
  productType: keyof typeof componentTypes;
  productId: number;
};

export default TOrderInfo;
