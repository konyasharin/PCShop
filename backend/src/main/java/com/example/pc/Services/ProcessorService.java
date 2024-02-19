package com.example.pc.Services;

import com.example.pc.IServices.IProcessorService;
import com.example.pc.Models.ProcessorModel;
import com.example.pc.dto.ProcessorDto;
import com.example.pc.repositories.ProcessorRepo;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.io.IOException;
import java.util.List;

@Service
public class ProcessorService implements IProcessorService {

    private final ProcessorRepo processorRepo;

    private static final Logger logger = LoggerFactory.getLogger(ProcessorService.class);

    @Autowired
    public ProcessorService(ProcessorRepo processorRepo) {
        this.processorRepo = processorRepo;
    }

    @Override
    public List<ProcessorModel> getProcessors() {
        return processorRepo.findAll();
    }

    @Override
    public ProcessorModel getProcessorById(Long id) {
        return processorRepo.findById(id).orElse(null);
    }

    @Override
    public ProcessorModel updateProcessor(ProcessorModel processorModel) {
        return processorRepo.save(processorModel);
    }

    @Override
    public void deleteProcessorById(Long id) {
        processorRepo.deleteById(id);
    }

    @Override
    public ProcessorModel createProcessor(ProcessorDto processorDto){

        try {
            byte[] imageData = processorDto.getImage().getBytes();
            return processorRepo.save(ProcessorModel.builder()
                    .brand(processorDto.getBrand())
                    .model(processorDto.getModel())
                    .description(processorDto.getDescription())
                    .price(processorDto.getPrice())
                    .country(processorDto.getCountry())
                    .clock_frequency(processorDto.getClock_frequency())
                    .turbo_frequency(processorDto.getTurbo_frequency())
                    .heat_dissipation(processorDto.getHeat_dissipation())
                    .image(imageData)
                    .build());
        }catch (IOException e){

            logger.error("Failed to convert Processor image to byte array", e);
            throw  new RuntimeException("Failed to convert Processor image to byte array", e);
        }
    }
}
