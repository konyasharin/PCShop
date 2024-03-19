type TComponent<T extends File | string> = {
  id: number;
  brand: string;
  model: string;
  country: string;
  price: number;
  description: string;
  image: T;
  amount: number;
};

export default TComponent;
