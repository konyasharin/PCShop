import TBuildPreview from 'types/TBuildPreview.ts';
import systemBlock from 'assets/sysblock.png';

const data: TBuildPreview[] = [
  {
    name: 'Колибри',
    img: systemBlock,
    description: {
      videoCard: 'RTX 4080 16GB',
      processor: 'Ryzen 7 4600',
      RAM: '32GB RAM',
      cooling: 'Водяное охлаждение',
    },
  },
  {
    name: 'Колибри',
    img: systemBlock,
    description: {
      videoCard: 'RTX 4080 16GB',
      processor: 'Ryzen 7 4600',
      RAM: '32GB RAM',
      cooling: 'Водяное охлаждение',
    },
  },
  {
    name: 'Колибри',
    img: systemBlock,
    description: {
      videoCard: 'RTX 4080 16GB',
      processor: 'Ryzen 7 4600',
      RAM: '32GB RAM',
      cooling: 'Водяное охлаждение',
    },
  },
];

function getLastBuilds(): Promise<TBuildPreview[]> {
  return new Promise(resolve => {
    setTimeout(() => {
      resolve([...data]);
    }, 1000);
  });
}

export default getLastBuilds;
