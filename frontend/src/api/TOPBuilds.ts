import TBuildPreview from 'types/TBuildPreview.ts';
import PCImg from 'assets/sysblock.png';

const data: TBuildPreview[] = [
  {
    description: {
      videoCard: 'RTX 4080 16GB',
      processor: 'Ryzen 7 4600',
      RAM: '32GB RAM',
      cooling: 'Водяное охлаждение',
    },
    name: 'Колибри',
    img: PCImg,
  },
  {
    description: {
      videoCard: 'RTX 4080 16GB',
      processor: 'Ryzen 7 4600',
      RAM: '32GB RAM',
      cooling: 'Водяное охлаждение',
    },
    name: 'Колибри',
    img: PCImg,
  },
  {
    description: {
      videoCard: 'RTX 4080 16GB',
      processor: 'Ryzen 7 4600',
      RAM: '32GB RAM',
      cooling: 'Водяное охлаждение',
    },
    name: 'Колибри',
    img: PCImg,
  },
];

function getTOPBuilds(): Promise<TBuildPreview[]> {
  return new Promise(resolve => {
    setTimeout(() => {
      resolve(data);
    }, 1000);
  });
}

export default getTOPBuilds;
