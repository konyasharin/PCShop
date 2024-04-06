import styles from './TOPBuilds.module.css';
import TOPBuildsBlock from './TOPBuildsBlock/TOPBuildsBlock.tsx';
import goldPrizeImg from 'assets/gprize.png';
import silverPrizeImg from 'assets/sprize.png';
import bronzePrizeImg from 'assets/bprize.png';
import mainPrize from 'assets/mainPrize.png';
import Container from 'components/Container/Container.tsx';
import React from 'react';
import TAssembly from 'types/TAssembly.ts';

type TOPBuildsProps = {
  TOPBuilds: TAssembly<string>[];
};

const TOPBuilds: React.FC<TOPBuildsProps> = props => {
  console.log(props.TOPBuilds);
  const TOPBuildsBlocks = props.TOPBuilds.map((block, i) => {
    let prizeImg = goldPrizeImg;
    switch (i) {
      case 0:
        prizeImg = goldPrizeImg;
        break;
      case 1:
        prizeImg = silverPrizeImg;
        break;
      case 2:
        prizeImg = bronzePrizeImg;
        break;
    }
    return (
      <TOPBuildsBlock
        className={styles.block}
        assembly={block}
        prizeImg={prizeImg}
      />
    );
  });

  return (
    <section className={styles.body}>
      <Container className={styles.container}>
        <h2 className={styles.name}>ТОП сборок</h2>
        <img src={mainPrize} alt={'mainPrize'} className={styles.mainPrize} />
        <div className={styles.blocks}>{...TOPBuildsBlocks}</div>
      </Container>
    </section>
  );
};

export default TOPBuilds;
