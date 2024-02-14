package com.example.pc.Services;

import com.example.pc.Models.ProcessorModel;
import com.example.pc.repositories.ProcessorRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class ProcessorService {

    private final ProcessorRepo processorRepo;

    @Autowired
    public ProcessorService(ProcessorRepo processorRepo) {
        this.processorRepo = processorRepo;
    }

    public List<ProcessorModel> getProcessors() {
        return processorRepo.findAll();
    }

    public ProcessorModel getProcessorById(Long id) {
        return processorRepo.findById(id).orElse(null);
    }

    public ProcessorModel saveProcessor(ProcessorModel processorModel) {
        return processorRepo.save(processorModel);
    }

    public void deleteProcessorById(Long id) {
        processorRepo.deleteById(id);
    }
}
