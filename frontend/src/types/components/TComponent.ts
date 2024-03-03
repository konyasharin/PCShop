type TComponent<T extends File | string> = {
  id: number;
  brand: string;
  model: string;
  county: string;
  price: number;
  description: string;
  image: T;
};

export default TComponent;
