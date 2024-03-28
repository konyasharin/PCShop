import Container from 'components/Container/Container.tsx';
import Scale from './Scale/Scale.tsx';
import { useState } from 'react';
import BuildsError from './BuildsError/BuildsError.tsx';
import PowerOfBuild from './PowerOfBuild/PowerOfBuild.tsx';
import ChooseComponents from './ChooseComponents/ChooseComponents.tsx';
import Input from 'components/inputs/Input/Input.tsx';
import styles from './PCBuildPage.module.css';
import Btn from 'components/btns/Btn/Btn.tsx';
import useBuild, {
  TUseBuildComponents,
  TUseBuildError,
} from 'hooks/useBuild.ts';
import componentTypes from 'enums/componentTypes.ts';
import EBuildBlockErrors from 'enums/EBuildBlockErrors.ts';
import TProduct from 'types/TProduct.ts';

function PCBuildPage() {
  const [buildName, setBuildName] = useState('');
  const {
    components,
    setComponent,
    toggleError,
    progressOfBuild,
    price,
    power,
    errors,
  } = useBuild();
  // const errors: TUseBuildError[] = [];
  // const power = 5;
  // const price = 100;
  // const progressOfBuild = 50;
  // const components: TUseBuildComponents = {
  //   videoCard: {
  //     currentProduct: null,
  //     currentErrorType: EBuildBlockErrors.Error,
  //   },
  //   processor: {
  //     currentProduct: null,
  //     currentErrorType: EBuildBlockErrors.Error,
  //   },
  // };
  // const setComponent = (
  //   newProduct: TProduct | null,
  //   componentType: keyof typeof componentTypes,
  // ) => {};
  const errorsBlocks = errors.map(error => {
    return (
      <BuildsError
        type={error.errorType}
        title={componentTypes[error.componentType]}
        description={error.errorDescription}
      />
    );
  });
  return (
    <div className={styles.body}>
      <Container>
        <h2 className={styles.build}>Ваша сборка</h2>
        <Input
          className={styles.input}
          value={buildName}
          placeholder={'Название сборки'}
          onChange={(newBuildName: string) => setBuildName(newBuildName)}
        />
        <Scale className={styles.scale} percents={progressOfBuild} />
        {...errorsBlocks}
        <ChooseComponents
          components={components}
          setComponent={setComponent}
          key={0}
        />
        <PowerOfBuild power={power} price={`${price}$`} />
        <Btn className={styles.button}>Cоздать пк</Btn>
      </Container>
    </div>
  );
}

export default PCBuildPage;
