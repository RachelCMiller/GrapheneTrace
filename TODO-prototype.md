# Prototype Development Plan - Team Selection Demo

## Assignment Context

**Course**: Second year university assignment
**Objective**: Create an impressive "head start" prototype before team selection tomorrow afternoon
**Goal**: Demonstrate technical competency and vision to attract the best teammates
**Strategy**: Build a visually striking, real-time demo that showcases understanding of both the technical and medical domain requirements

## Core Requirements Recap
Based on @Requirements/ClientRequest.md:
- Process 32x32 pressure matrices from CSV data (values 1-255)
- Three user types: Patient, Clinician, Admin
- Detect high pressure regions and generate alerts
- Calculate Peak Pressure Index and Contact Area %
- Time-based visualization and reporting
- Comment system with timestamp association

## Prototype Strategy: Maximum Visual Impact

### Demo Flow That Will Blow Minds
1. **App starts** → Console clears, shows professional header
2. **Heat map appears** → Real-time 32x32 colored ASCII visualization
3. **Realistic simulation** → "Person" sitting with anatomically correct pressure patterns
4. **Gradual pressure buildup** → Pressure increases in ischial tuberosity (sit bones)
5. **Alert trigger** → Screen flashes red, console beeps, "HIGH PRESSURE DETECTED!"
6. **Metrics dashboard** → Real-time Peak Pressure Index and Contact Area %
7. **Position shift** → Simulate person moving, pressure redistributes, alert clears
8. **Professional polish** → Smooth updates, clean UI, medical terminology

### Why This Works
- **Immediate comprehension**: Anyone can see what it does in 10 seconds
- **Technical depth**: Shows real-time processing, threading, data structures
- **Domain expertise**: Demonstrates understanding of pressure ulcer prevention
- **Production feel**: Looks like a real medical device interface
- **Scalability hint**: Easy to imagine expanding to multiple patients

## Implementation Priority (Build in This Order)

### Phase 1: Core Foundation (2-3 hours)
**Priority 1: Heat Map Console Renderer**
- [ ] `Console/Display/HeatMapRenderer.cs`
  - ASCII/Unicode heat map with ANSI colors
  - Map pressure values (1-255) to color intensity
  - Smooth real-time updates (10+ FPS)
  - Professional border and labels
  - Legend showing pressure ranges

**Priority 2: Mock Sensor with Anatomical Accuracy**
- [ ] `Services/Mocking/MockPressureDataGenerator.cs`
  - Generate realistic sitting patterns
  - Ischial tuberosity pressure points (highest areas)
  - Thigh distribution patterns
  - Gradual pressure buildup over time
  - Weight shifting simulation

### Phase 2: Real-Time Processing (1-2 hours)
**Priority 3: Live Alert System**
- [ ] `Services/Processing/AlertEvaluationService.cs`
  - Real-time threshold monitoring
  - Visual alerts (screen flash, color changes)
  - Audio alerts (console beeps)
  - Alert persistence and clearing
  - Configurable thresholds

**Priority 4: Metrics Dashboard**
- [ ] `Console/Display/MetricDashboard.cs`
  - Real-time Peak Pressure Index calculation
  - Contact Area % computation
  - Split-screen layout (heat map + metrics)
  - Trend indicators (increasing/decreasing)

### Phase 3: Polish & Wow Factor (1 hour)
**Priority 5: Professional Presentation**
- [ ] `Console/Display/UserInterface.cs`
  - Startup splash screen with project branding
  - Smooth transitions and animations
  - Keyboard controls (space to pause, 'r' to reset)
  - Status bar with simulation info
  - Clean shutdown sequence

**Priority 6: Demo Scenarios**
- [ ] `Services/Mocking/ScenarioManager.cs`
  - Predefined demo scenarios
  - Normal sitting → pressure buildup → alert → relief
  - Multiple "patients" with different patterns
  - Clinically realistic timelines

## Technical Implementation Notes

### Heat Map Visualization
```
Colors: Blue (low) → Green → Yellow → Orange → Red (high)
Characters: Use Unicode blocks ▓▒░ for texture
Update rate: 10 FPS minimum for smooth experience
Size: 32x32 but displayed larger for visibility
```

### Mock Data Patterns
```
Anatomical accuracy:
- Ischial tuberosity: Rows 12-20, Cols 8-12 and 20-24 (highest pressure)
- Thighs: Broader distribution around sitting area
- Gradual buildup: +2-5 pressure units per second
- Movement simulation: Shift pressure zones every 30-60 seconds
```

### Console Layout
```
╭─────────────────────────────────────────────────────────╮
│                 GrapheneTrace Sensore                   │
│                 Pressure Monitoring                     │
├─────────────────────────┬───────────────────────────────┤
│     PRESSURE MAP        │        LIVE METRICS           │
│  [32x32 colored grid]   │  Peak Pressure: 186 mmHg     │
│                         │  Contact Area:  73%           │
│                         │  Alert Status:  NORMAL        │
│                         │  Last Update:   14:23:17      │
├─────────────────────────┴───────────────────────────────┤
│ [SPACE] Pause  [R] Reset  [Q] Quit  [1-3] Switch User  │
╰─────────────────────────────────────────────────────────╯
```

## Demo Script for Team Selection

### Opening Hook (30 seconds)
"I've been working on our GrapheneTrace project and built a real-time pressure monitoring prototype. This simulates the actual Sensore mat from the requirements - watch the pressure buildup."

### Technical Showcase (60 seconds)
- Start the app → Show smooth real-time visualization
- Point out anatomically accurate pressure patterns
- Trigger alert → Explain threshold detection
- Show metrics calculating in real-time
- Demonstrate multiple scenarios

### Vision Pitch (30 seconds)
"This is just the visualization layer. I've architected the full system with PostgreSQL database, user management, API endpoints, and comprehensive testing. We can have a production-ready system that'll impress the professors."

## Files to Focus On

### Must-Have for Demo
1. `Console/Display/HeatMapRenderer.cs` - The star of the show
2. `Services/Mocking/MockPressureDataGenerator.cs` - Realistic data
3. `Services/Processing/AlertEvaluationService.cs` - Alert system
4. `Console/Display/MetricDashboard.cs` - Live metrics
5. `Program.cs` - Orchestrate everything

### Supporting Files
6. `Core/Models/PressureMap.cs` - Already exists, may need minor updates
7. `Console/Display/ColorMapper.cs` - Pressure-to-color conversion
8. `Services/Mocking/AnatomicalPatterns.cs` - Realistic sitting patterns

## Success Metrics

**Demo is successful if:**
- [ ] Heat map updates smoothly without flickering
- [ ] Pressure patterns look medically realistic
- [ ] Alerts trigger at appropriate thresholds
- [ ] Metrics calculate correctly and update in real-time
- [ ] Overall presentation looks professional and polished

**Stretch goals:**
- [ ] Multiple patient scenarios
- [ ] Keyboard interaction for demo control
- [ ] Export functionality to show data persistence
- [ ] Performance metrics (processing speed, FPS)

## Timeline

**Today (Before Team Selection)**
- 2-3 hours: Core functionality (heat map + mock data)
- 1-2 hours: Alert system and metrics
- 1 hour: Polish and demo preparation

**Total: 4-6 hours for a prototype that will absolutely dominate**

Remember: The goal isn't to build everything - it's to build something so impressive that the best students will want to work with you. Focus on visual impact and smooth execution over feature completeness.