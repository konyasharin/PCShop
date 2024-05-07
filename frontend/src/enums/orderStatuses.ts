const orderStatuses = {
  accepted: 'Принят',
  inProcessing: 'В обработке',
  onTheWay: 'В пути',
  delivered: 'Доставлен',
} as const;

export default orderStatuses;
