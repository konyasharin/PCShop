type TAssembly<T extends File | string> = {
  id: number;
  name: string;
  image: T;
  likes: number;
  creationTime: Date;
  price: number;
  power: number;
  computerCaseId: number;
  coolerId: number;
  motherBoardId: number;
  powerUnitId: number;
  processorId: number;
  ramId: number;
  ssdId: number;
  videoCardId: number;
};

export default TAssembly;
