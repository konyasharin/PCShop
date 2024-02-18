package com.example.pc.controllers;

import com.example.pc.Models.ProcessorModel;
import com.example.pc.Services.ProcessorService;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

@RestController
@RequestMapping("/api")
public class ProcessorAPI {

    private static final Logger logger = LoggerFactory.getLogger(ProcessorAPI.class);

    @Autowired
    private ProcessorService processorService;

    @PostMapping("/createComponent/Processor")
    public ResponseEntity<Map<String, Object>> createProcessor(@RequestParam("brand") String brand,
                                                               @RequestParam("model") String model,
                                                               @RequestParam("description") String description,
                                                               @RequestParam("price") Integer price,
                                                               @RequestParam("country") String country,
                                                               @RequestParam("clock_frequency") Integer clock_frequency,
                                                               @RequestParam("turbo_frequency") Integer turbo_frequency,
                                                               @RequestParam("heat_dissipation") Integer heat_dissipation,
                                                               @RequestParam("image") MultipartFile image) {

        try {

            if (image.isEmpty()) {
                logger.warn("No image provided");
                return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
            }

            ProcessorModel processorModel = new ProcessorModel();
            processorModel.setBrand(brand);
            processorModel.setModel(model);
            processorModel.setDescription(description);
            processorModel.setPrice(price);
            processorModel.setCountry(country);
            processorModel.setClock_frequency(clock_frequency);
            processorModel.setTurbo_frequency(turbo_frequency);
            processorModel.setHeat_dissipation(heat_dissipation);
            processorModel.setImage(image.getBytes());

            ProcessorModel savedProcessorModel = processorService.addProcessor(processorModel);

            Map<String, Object> ProcessorResponse = new HashMap<>();
            ProcessorResponse.put("type", "Processor");
            ProcessorResponse.put("id", savedProcessorModel.getId());
            ProcessorResponse.put("ComponentData", savedProcessorModel);

            logger.info("Processor created successfully: {}", ProcessorResponse);

            return new ResponseEntity<>(ProcessorResponse, HttpStatus.CREATED);

        } catch (IOException e) {
            e.printStackTrace();

            logger.error("Failed to create Processor", e);

            return new ResponseEntity<>(HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }
}
