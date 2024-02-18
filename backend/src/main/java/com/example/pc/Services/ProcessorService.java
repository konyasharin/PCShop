package com.example.pc.Services;

import com.example.pc.IServices.IProcessorService;
import com.example.pc.Models.ProcessorModel;
import com.example.pc.repositories.ProcessorRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class ProcessorService implements IProcessorService {

    private final ProcessorRepo processorRepo;

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
    public ProcessorModel addProcessor(ProcessorModel processorModel) {
        return processorRepo.save(processorModel);
    }

    @Override
    public void deleteProcessorById(Long id) {
        processorRepo.deleteById(id);
    }
}
