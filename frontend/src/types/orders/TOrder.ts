import orderStatuses from 'enums/orderStatuses.ts';

type TOrder = {
  orderId: number;
  orderStatus: keyof typeof orderStatuses;
  userId: number;
};

export default TOrder;
