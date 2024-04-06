import { useEffect, useState } from 'react';
import TOneOfComponents from 'types/components/TOneOfComponents.ts';
import TAssembly from 'types/TAssembly.ts';
import useLike from 'hooks/useLike.ts';
import getComponent from 'api/components/getComponent.ts';

function useBuildCard(assembly: TAssembly<string>) {
  const [videoCard, setVideoCard] = useState<TOneOfComponents<string> | null>(
    null,
  );
  const [processor, setProcessor] = useState<TOneOfComponents<string> | null>(
    null,
  );
  const [RAM, setRAM] = useState<TOneOfComponents<string> | null>(null);
  const [cooler, setCooler] = useState<TOneOfComponents<string> | null>(null);
  const {
    likes,
    isActive: likeIsActive,
    setIsActiveHandle: setLikeIsActive,
  } = useLike(assembly.likes);
  useEffect(() => {
    async function getMainComponents() {
      setVideoCard(
        (await getComponent('videoCard', assembly.videoCardId)).data,
      );
      setProcessor(
        (await getComponent('processor', assembly.processorId)).data,
      );
      setRAM((await getComponent('RAM', assembly.ramId)).data);
      setCooler((await getComponent('cooler', assembly.coolerId)).data);
    }
    void getMainComponents();
  }, []);

  return {
    videoCard,
    processor,
    RAM,
    cooler,
    likes,
    likeIsActive,
    setLikeIsActive,
  };
}

export default useBuildCard;
