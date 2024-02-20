import styles from './TOPBuilds.module.css';
import TOPBuildsBlock from './TOPBuildsBlock/TOPBuildsBlock.tsx';
import goldPrizeImg from 'assets/gprize.png';
import silverPrizeImg from 'assets/sprize.png';
import bronzePrizeImg from 'assets/bprize.png';
import mainPrize from 'assets/mainPrize.png';
import Container from 'components/Container/Container.tsx';
import { useEffect, useState } from 'react';
import TBuildPreview from 'types/TBuildPreview.ts';
import getTOPBuilds from 'api/TOPBuilds.ts';

function TOPBuilds() {
  const [TOPBuilds, setTOPBuilds] = useState<TBuildPreview[]>([]);
  useEffect(() => {
    getTOPBuilds().then(data => {
      setTOPBuilds(data);
    });
  }, []);
  const TOPBuildsBlocks = TOPBuilds.map((block, i) => {
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
        description={block.description}
        img={block.img}
        prizeImg={prizeImg}
        name={block.name}
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
}

export default TOPBuilds;
