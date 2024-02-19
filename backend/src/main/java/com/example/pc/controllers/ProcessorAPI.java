package com.example.pc.controllers;

import com.example.pc.Models.ProcessorModel;
import com.example.pc.Services.ProcessorService;
import com.example.pc.dto.ProcessorDto;
import lombok.AllArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

@RestController
@AllArgsConstructor
public class ProcessorAPI {

    private static final Logger logger = LoggerFactory.getLogger(ProcessorAPI.class);

    @Autowired
    private ProcessorService processorService;

    @PostMapping("/createComponent/Processor")
    public ResponseEntity<ProcessorModel> createProcessor(@RequestBody ProcessorDto processorDto) {

        MultipartFile image = processorDto.getImage();

        if (image == null || image.isEmpty()) {
            logger.warn("No image provided");
            return new ResponseEntity<>(HttpStatus.BAD_REQUEST);
        }

        logger.info("Processor created successfully: {}", processorDto);

        return new ResponseEntity<>(processorService.createProcessor(processorDto),
                HttpStatus.CREATED);
    }
}
