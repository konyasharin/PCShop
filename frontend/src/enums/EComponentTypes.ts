enum EComponentTypes {
  videoCard = 'videoCard',
  processor = 'processor',
  /*cooling = 'cooling',
  RAM = 'RAM',
  motherBoard = 'motherBoard',
  HDD = 'HDD',
  SSD = 'SSD',
  case = 'case',
  powerSupply = 'powerSupply',*/
}

export const componentTypesTitles = {
  videoCard: 'Видеокарта',
  processor: 'Процессор',
} as const;

export default EComponentTypes;
